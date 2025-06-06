using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class Spawner : MonoBehaviour
{
    [SerializeField] private List<SpawnPoint> _spawnPoints = new();
    [SerializeField] private List<EnemyPrefabMapping> _enemyPrefabs = new();

    private WaitForSeconds _spawnDelay;
    private Dictionary<EnemyType, ObjectPool<EnemyBase>> _pools = new();

    private void Awake()
    {
        InitializePools();
    }

    private void Start()
    {
        _spawnDelay = new WaitForSeconds(Settings.SpawnInterval);
        StartCoroutine(SpanwRoutine());
    }

    private void OnDestroy()
    {
        foreach (var pool in _pools.Values)
            pool?.Dispose();
    }

    public void Release(EnemyBase enemy)
    {
        if(_pools.TryGetValue(enemy.Type, out var pool))
            pool.Release(enemy);
    }

    private void InitializePools()
    {
        foreach (var mapping in _enemyPrefabs)
        {
            _pools[mapping.Type] = new ObjectPool<EnemyBase>(
                () => CreateEnemy(mapping.Prefab),
                OnGet,
                OnRelease,
                OnDestroyEnemy,
                true,
                defaultCapacity: Settings.DefaultPoolCapacity,
                maxSize: Settings.MaxPoolSize
            );
        }
    }

    private EnemyBase CreateEnemy(EnemyBase prefab)
    {
        EnemyBase enemy = Instantiate(prefab);
        enemy.Deactivated += Release;
        return enemy;
    }

    private void OnGet(EnemyBase enemy) =>
        enemy.gameObject.SetActive(true);

    private void OnRelease(EnemyBase enemy) =>
        enemy.gameObject.SetActive(false);

    private void OnDestroyEnemy(EnemyBase enemy)
    {
        if (enemy != null)
        {
            enemy.Deactivated -= Release;
            Destroy(enemy.gameObject);
        }
    }

    private IEnumerator SpanwRoutine()
    {
        while (enabled)
        {
            yield return _spawnDelay;
            SpawnAtRandomPoint();
        }
    }

    private void SpawnAtRandomPoint()
    {
        var point = _spawnPoints[UnityEngine.Random.Range(0, _spawnPoints.Count)];

        if(_pools.TryGetValue(point.Type, out var pool))
        {
            EnemyBase enemy = pool.Get();
            enemy.Initialize(point.Target.transform);
            enemy.Activate(point.Position);
        }
    }
}
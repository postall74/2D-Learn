using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class Spawner : MonoBehaviour
{
    [SerializeField] private List<SpawnPoint> _spawnPoints = new();
    [SerializeField] private List<EnemyPrefabMapping> _enemyPrefabs = new();

    private WaitForSeconds _delay;
    private Dictionary<EnemyType, ObjectPool<Enemy>> _pools = new();

    private void Awake()
    {
        InitializePools();
    }

    private void Start()
    {
        _delay = new WaitForSeconds(Settings.SpawnInterval);
        StartCoroutine(SpanwRoutine());
    }

    private void OnDestroy()
    {
        foreach (var pool in _pools.Values)
            pool?.Dispose();
    }

    public void Release(Enemy enemy)
    {
        if(_pools.TryGetValue(enemy.Type, out var pool))
            pool.Release(enemy);
    }

    private void InitializePools()
    {
        foreach (var mapping in _enemyPrefabs)
        {
            _pools[mapping.Type] = new ObjectPool<Enemy>(
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

    private Enemy CreateEnemy(Enemy prefab)
    {
        Enemy enemy = Instantiate(prefab);
        enemy.OnDeactivated += Release;
        return enemy;
    }

    private void OnGet(Enemy enemy) =>
        enemy.gameObject.SetActive(true);

    private void OnRelease(Enemy enemy) =>
        enemy.gameObject.SetActive(false);

    private void OnDestroyEnemy(Enemy enemy)
    {
        if (enemy != null)
        {
            enemy.OnDeactivated -= Release;
            Destroy(enemy.gameObject);
        }
    }

    private IEnumerator SpanwRoutine()
    {
        while (enabled)
        {
            yield return _delay;
            SpawnAtRandomPoint();
        }
    }

    private void SpawnAtRandomPoint()
    {
        var point = _spawnPoints[UnityEngine.Random.Range(0, _spawnPoints.Count)];

        if(_pools.TryGetValue(point.Type, out var pool))
        {
            Enemy enemy = pool.Get();
            enemy.Initialize(point.Type, point.Target.transform);
            enemy.Activate(point.Position);
        }
    }
}
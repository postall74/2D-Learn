using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class Spawner : MonoBehaviour
{
    [SerializeField] private List<SpawnPoint> _spawnPoints = new();
    [SerializeField] private Enemy _enemyPrefab;

    private WaitForSeconds _delay;

    //public event Action<Vector3, Vector3> OnSpawnRequested;

    private void Awake()
    {
        _pool = new ObjectPool<Enemy>(
            CreateEnemy,
            OnGet,
            OnRelease,
            OnDestroyEnemy,
            true,
            Settings.DefaultPoolCapacity,
            Settings.MaxPoolSize);
    }

    private void Start()
    {
        _delay = new WaitForSeconds(Settings.SpawnInterval);
        StartCoroutine(SpanwRoutine());
    }
    private ObjectPool<Enemy> _pool;

    private void OnDestroy() =>
        _pool?.Dispose();

    public Enemy Get() =>
        _pool.Get();

    public void Release(Enemy enemy) =>
        _pool.Release(enemy);

    private Enemy CreateEnemy()
    {
        var enemy = Instantiate(_enemyPrefab);
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
        while(enabled)
        {
            yield return _delay;
            SpawnAtRandomPoint();
        }
    }

    private void SpawnAtRandomPoint()
    {
        var index = UnityEngine.Random.Range(0, _spawnPoints.Count);
        var point = _spawnPoints[index];
        Enemy enemy = _pool.Get();
        enemy.Activate(point.Position, point.Direction);
    }
}
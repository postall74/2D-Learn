using System;
using UnityEngine;
using UnityEngine.Pool;

public class EnemyPool : MonoBehaviour
{
    [SerializeField] private Enemy _enemyPrefab;

    private ObjectPool<Enemy> _pool;

    public event Action<IEnemy> OnEnemyCreated;

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

    private Enemy CreateEnemy()
    {
        var enemy = Instantiate(_enemyPrefab);
        enemy.Initialize(new Movement(), new Animation());
        enemy.OnDeactivated += Release;
        OnEnemyCreated?.Invoke(enemy);
        return enemy;
    }

    private void OnGet(Enemy enemy) => 
        enemy.gameObject.SetActive(true);

    private void OnRelease(Enemy enemy) => 
        enemy.gameObject.SetActive(false);

    private void OnDestroyEnemy(Enemy enemy)
    {
        if (enemy != null && enemy.gameObject != null)
            Destroy(enemy.gameObject);
    }

    public Enemy Get() => 
        _pool.Get();

    public void Release(Enemy enemy) => 
        _pool.Release(enemy);

    private void OnDestroy() => 
        _pool?.Dispose();
}
using System;
using UnityEngine;
using UnityEngine.Pool;

public class EnemyPool : MonoBehaviour
{
    [SerializeField] private Enemy _enemyPrefab;
    [SerializeField] private int _defaultCapacity = 10;
    [SerializeField] private int _maxSize = 100;

    private ObjectPool<Enemy> _pool;
    private IMovement _movement = new Movement();
    private IAnimation _animation = new Animation();

    public event Action<IEnemy> OnEnemyCreated;

    private void Awake()
    {
        _pool = new ObjectPool<Enemy>
            (
                CreateEnemy,
                OnGet,
                OnRelease,
                OnDestroy,
                true,
                _defaultCapacity,
                _maxSize
            );
    }

    public Enemy Get() =>
        _pool.Get();

    public void Release(Enemy enemy) =>
        _pool.Release(enemy);

    private Enemy CreateEnemy()
    {
        Enemy enemy = Instantiate(_enemyPrefab);
        enemy.Initialize(_movement, _animation);
        enemy.OnDeactivated += Release;
        OnEnemyCreated?.Invoke(enemy);
        return enemy;
    }

    private void OnGet(Enemy enemy) =>
        enemy.gameObject.SetActive(true);

    private void OnRelease(Enemy enemy) =>
        enemy.gameObject.SetActive(false);

    private void OnDestroy(Enemy enemy)
    {
        Destroy(enemy.gameObject);
    }
}
using UnityEngine;

public class GameRouter : MonoBehaviour
{
    [SerializeField] private Spawner _spawner;
    [SerializeField] private EnemyPool _pool;

    private void OnEnable()
    {
        _spawner.OnSpawnRequested += HandleSpawnRequest;
    }

    private void OnDisable()
    {
        _spawner.OnSpawnRequested -= HandleSpawnRequest;
    }

    private void HandleSpawnRequest(Vector3 position, Vector3 direction)
    {
        Enemy enemy = _pool.Get();
        enemy.Activate(position, direction);
    }
}

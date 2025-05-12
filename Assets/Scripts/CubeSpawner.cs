using System.Collections;
using UnityEngine;

public class CubeSpawner : MonoBehaviour
{
    [SerializeField] private CubePool _pool;
    [SerializeField] private Settings _settings;
    [SerializeField] private Transform _spawnArea;

    private WaitForSeconds _spawnInterval;

    private void Awake()
    {
        ValidateReferences();
        HandleMissingPool();
    }

    private void ValidateReferences()
    {
        if (_spawnArea == null)
        {
            _spawnArea = GetComponent<Transform>();
#if UNITY_EDITOR
            Debug.LogError($"SpawnArea not assigned, using self transform");
#endif
        }
    }

    private void HandleMissingPool()
    {
        if (_pool == null)
        {
            _pool = gameObject.AddComponent<CubePool>() ?? gameObject.GetComponent<CubePool>();
#if UNITY_EDITOR
            Debug.LogError($"Haven't link to CubePool. Create new component in this object.");
#endif
        }
    }

    private void Start()
    {
        _spawnInterval = new WaitForSeconds(Random.Range(GameConstants.MinSpawnDelay, GameConstants.MaxSpawnDelay));

        if (CheckDependencies() == false)
            return;

        StartCoroutine(SpawnCoroutine());
    }

    private bool CheckDependencies()
    {
        if (_pool == null)
        {
#if UNITY_EDITOR
            Debug.LogError($"CubePool reference is missing!");
#endif
            return false;
        }

        if (_settings == null)
        {
#if UNITY_EDITOR
            Debug.LogError($"Settings reference is missing!");
#endif
            return false;
        }

        return true;
    }

    private IEnumerator SpawnCoroutine()
    {
        while (enabled)
        {
            yield return _spawnInterval;

            if(enabled == false) 
                yield break;

            Spawn();
        }
    }

    private void Spawn()
    {
        CubeBehaviour cube = _pool.GetCube();

        if (cube == null)
        {
#if UNITY_EDITOR
            Debug.LogError($"Falid to get cube from pool!");
#endif
            return;
        }

        cube.Initialize(_settings);
        cube.transform.position = CalculateSpawnPosition();
    }

    private Vector3 CalculateSpawnPosition()
    {
        var scale = _spawnArea.localScale;

        return _spawnArea.position + new Vector3(
            Random.Range(-scale.x, scale.x) * GameConstants.BasePlaneSize / 2f,
            GameConstants.SpawnHeightOffset,
            Random.Range(-scale.z, scale.z) * GameConstants.BasePlaneSize / 2f
        );
    }
}

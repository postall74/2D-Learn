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
        if (_pool == null)
        {
            gameObject.AddComponent<CubePool>();
#if UNITY_EDITOR
            Debug.LogError($"Don't link to CubePool. Create new component in this object.");
#endif
        }

        if (_spawnArea == null)
        {
            _spawnArea = GetComponent<Transform>();
#if UNITY_EDITOR
            Debug.LogError($"Don't link to Spawn Area. Added link to this gameobject.");
#endif
        }
    }

    private void Start()
    {
        StartCoroutine(SpawnCoroutine());
    }

    private IEnumerator SpawnCoroutine()
    {
        while(true)
        { 
            Spawn();
            yield return new WaitForSeconds(Random.Range(
                InputConstants.MinSpawnTimeCube,
                InputConstants.MaxSpawnTimeCube
            ));
        }
    }

    private void Spawn()
    {
        CubeBehaviour cube = _pool.Pool.Get();
        cube.Initialize(_pool.Pool, _settings);
        cube.transform.position = CalculateSpawnPosition();
    }

    private Vector3 CalculateSpawnPosition()
    {
        var scale = _spawnArea.localScale;

        return _spawnArea.position + new Vector3(
            Random.Range(-scale.x, scale.x) * InputConstants.BasePlaneSize / 2f,
            InputConstants.SpawnHeightOffset,
            Random.Range(-scale.z, scale.z) * InputConstants.BasePlaneSize / 2f
        );
    }
}

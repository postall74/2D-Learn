using UnityEngine;
using UnityEngine.Pool;

public class CubePool : MonoBehaviour
{
    [SerializeField] private CubeBehaviour _cube;
    [SerializeField, Min(100)] private int _defaultCapacity = 1000;
    [SerializeField, Min(100)] private int _maxSize = 10000;

    private ObjectPool<CubeBehaviour> _pool;
    private bool _isInitialized = false;

    public CubeBehaviour GetCube()
    {
        if (_isInitialized == false)
        {
#if UNITY_EDITOR
            Debug.LogError($"Pool is not initialized!");
#endif      
            return null;
        }

        return _pool.Get();
    }

    private void Awake()
    {
        if (_cube == null)
        {
#if UNITY_EDITOR
            Debug.LogError($"Cube prefab is not assigned in CubePool");
#endif
            enabled = false;
            return;
        }

        InitializePool();
    }

    private void InitializePool()
    {
        _pool = new ObjectPool<CubeBehaviour>(
            createFunc: CreateCube,
            actionOnGet: EnableCube,
            actionOnRelease: DisableCube,
            actionOnDestroy: DestroyCube,
            defaultCapacity: _defaultCapacity,
            maxSize: _maxSize
        );

        _isInitialized = true;
    }

    private CubeBehaviour CreateCube()
    {
        var cube = Instantiate(_cube);
        cube.OnReleaseRequested += ReleaseCube;
        return cube;
    }

    private void ReleaseCube(CubeBehaviour cube) =>
        _pool.Release(cube);

    private void EnableCube(CubeBehaviour cube) =>
        cube.gameObject.SetActive(true);

    private void DisableCube(CubeBehaviour cube) =>
        cube.gameObject.SetActive(false);

    private void DestroyCube(CubeBehaviour cube) =>
        Destroy(cube.gameObject);
}

using UnityEngine;
using UnityEngine.Pool;

public class CubePool : MonoBehaviour
{
    [SerializeField] private CubeBehaviour _cube;
    [SerializeField] private int _defaultCapacity = 1000;
    [SerializeField] private int _maxSize = 10000;

    private ObjectPool<CubeBehaviour> _pool;

    public ObjectPool<CubeBehaviour> Pool => _pool;

    private void Awake()
    {
        _pool = new ObjectPool<CubeBehaviour>
            (
                createFunc: CreateCube,
                actionOnGet: OnGetCube,
                actionOnRelease: OnReleaseCube,
                actionOnDestroy: OnDestroyCube,
                defaultCapacity: _defaultCapacity,
                maxSize: _maxSize
            );
    }

    private CubeBehaviour CreateCube() => Instantiate(_cube);
    private void OnGetCube(CubeBehaviour cube) => cube.gameObject.SetActive(true);
    private void OnReleaseCube(CubeBehaviour cube) => cube.gameObject.SetActive(false);
    private void OnDestroyCube(CubeBehaviour cube) => Destroy(cube.gameObject);
}

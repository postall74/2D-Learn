using UnityEngine;

public class CubeSpawner : MonoBehaviour
{
    [SerializeField] private GameObject _prefab;
    [SerializeField] private int _minSpawnCount = 2;
    [SerializeField] private int _maxSpawnCount = 6;
    [SerializeField, Range(0f, 1f)] private float _scaleFactor;

    public event System.Action<GameObject[]> OnCubeSpawned;

    private void OnEnable()
    {
        if (TryGetComponent(out Cube cube))
            cube.OnSplitRequested += HandleSplit;
    }

    private void OnDisable()
    {
        if (TryGetComponent(out Cube cube))
            cube.OnSplitRequested -= HandleSplit;
    }

    private void HandleSplit(float newChance)
    {
        int count = Random.Range(_minSpawnCount, _maxSpawnCount + 1);
        GameObject[] newCubes = new GameObject[count];

        for (int i = 0; i < count; i++)
        {
            GameObject cube = Instantiate(_prefab, transform.position, Random.rotation);
            cube.transform.localScale = transform.localScale * _scaleFactor;
            
            if (cube.TryGetComponent(out Cube cubeComponent))
                cubeComponent.Initialize(newChance);

                newCubes[i] = cube;
        }

        OnCubeSpawned?.Invoke(newCubes);
    }
}
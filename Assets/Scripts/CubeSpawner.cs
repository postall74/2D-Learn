using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CubeSpawner : MonoBehaviour
{
    [SerializeField] private Cube _cubePrefab;
    [SerializeField] private int _minSpawnCount = 2;
    [SerializeField] private int _maxSpawnCount = 6;
    [SerializeField, Range(0f, 1f)] private float _scaleFactor = 0.5f;

    public event System.Action<List<Rigidbody>> OnRigidbodiesSpawned;
    public event System.Action<List<Renderer>> OnRendererSpawned;

    private readonly Dictionary<Cube, System.Action<float>> _handlers = new();

    private void OnEnable()
    {
        if (_cubePrefab != null)
            _cubePrefab.SplitRequested += Spawn;
    }

    private void OnDisable()
    {
        if (_cubePrefab != null)
            _cubePrefab.SplitRequested -= Spawn;
    }

    private void Spawn(float newChance)
    {
        int count = Random.Range(_minSpawnCount, _maxSpawnCount + 1);
        List<Rigidbody> rigidbodies = new List<Rigidbody>();
        List<Renderer> renderers = new List<Renderer>();

        for (int i = 0; i < count; i++)
        {
            Cube cube = Instantiate(_cubePrefab, transform.position, Random.rotation);
            cube.transform.localScale = transform.localScale * _scaleFactor;
            cube.Initialize(newChance);

            Rigidbody rigidbody = cube.GetComponent<Rigidbody>() ?? cube.AddComponent<Rigidbody>();
            rigidbody.useGravity = true;
            rigidbodies.Add(rigidbody);

            if (cube.TryGetComponent(out Renderer renderer))
                renderers.Add(renderer);
        }

        OnRigidbodiesSpawned?.Invoke(rigidbodies);
        OnRendererSpawned?.Invoke(renderers);
    }
}
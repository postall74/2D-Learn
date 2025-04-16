using UnityEngine;

public class CubeLogic : MonoBehaviour, IClickable
{
    [SerializeField] private GameObject _prefab;
    [SerializeField] private Material[] _materials;
    [SerializeField] private float _explosionForce = 200f;
    [SerializeField] private float _explosionRadius = 2f;
    [SerializeField] private int _minSpawnCount = 2;
    [SerializeField] private int _maxSpawnCount = 6;
    [SerializeField, Range(0f, 1f)] private float _splitChance = 1f;
    [SerializeField, Range(0f, 1f)] private float _splitDecay = 0.5f;
    [SerializeField, Range(0f, 1f)] private float _scaleNewCube = 0.5f;

    private void SpawnChildren()
    {
        int count = Random.Range(_minSpawnCount, _maxSpawnCount + 1);
        Vector3 origin = transform.position;

        for (int i = 0; i < count; i++)
        {
            GameObject newCube = Instantiate(_prefab, origin, Random.rotation);
            newCube.transform.localScale = transform.localScale * _scaleNewCube;
            newCube.GetComponent<Renderer>().material = _materials[Random.Range(0, _materials.Length)];

            if (!newCube.TryGetComponent(out Rigidbody rb))
                rb = newCube.AddComponent<Rigidbody>();

            rb.useGravity = true;
            rb.AddExplosionForce(_explosionForce, origin, _explosionRadius);

            if (newCube.TryGetComponent(out CubeLogic logic))
            {
                logic.SetSplitChance(_splitChance * _splitDecay);
            }
        }
    }

    public void SetSplitChance(float newChance)
    {
        _splitChance = newChance;
    }

    public void OnClick()
    {
        if (Random.value <= _splitChance)
        {
            SpawnChildren();
            _splitChance *= _splitDecay;
        }

        Destroy(gameObject);
    }
}
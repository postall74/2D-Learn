using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(CubeExploder))]
public class CubeExploder: MonoBehaviour
{
    [SerializeField] private float _explosionForce = 200f;
    [SerializeField] private float _explosionRadius = 2f;

    private void OnEnable()
    {
        if (TryGetComponent(out CubeSpawner spawner))
            spawner.OnCubeSpawned += ExplodeCubes;
    }

    private void OnDisable()
    {
        if (TryGetComponent(out CubeSpawner spawner))
            spawner.OnCubeSpawned -= ExplodeCubes;
    }

    private void ExplodeCubes(GameObject[] cubes)
    {
        foreach (var cube in cubes)
        {
            if (cube.TryGetComponent(out Rigidbody rigidbody) == false)
                rigidbody = cube.AddComponent<Rigidbody>();

            rigidbody.useGravity = true;
            rigidbody.AddExplosionForce(_explosionForce, transform.position, _explosionRadius);
        }
    }
}

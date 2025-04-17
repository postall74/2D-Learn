using UnityEngine;

[RequireComponent(typeof(CubeExploder))]
public class CubeMaterialSetter: MonoBehaviour
{
    [SerializeField] private Material[] _materials;

    private void OnEnable()
    {
        if (TryGetComponent(out CubeSpawner spawner))
            spawner.OnCubeSpawned += SetRandomMaterial;
    }

    private void OnDisable()
    {
        if (TryGetComponent(out CubeSpawner spawner))
            spawner.OnCubeSpawned -= SetRandomMaterial;
    }

    private void SetRandomMaterial(GameObject[] cubes)
    {
        //if (_materials.Length == 0)
        //    return;

        foreach (var cube in cubes)
            if (cube.TryGetComponent(out Renderer renderer))
                renderer.material = _materials[Random.Range(0, _materials.Length)];

    }
}
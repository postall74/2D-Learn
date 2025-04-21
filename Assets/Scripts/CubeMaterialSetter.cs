using System.Collections.Generic;
using UnityEngine;

public class CubeMaterialSetter : MonoBehaviour
{
    [SerializeField] private Material[] _materials;

    private void OnEnable()
    {
        CubeCreatedEvent.OnCubesCreated += SetRandomMaterial;
    }

    private void OnDisable()
    {
        CubeCreatedEvent.OnCubesCreated -= SetRandomMaterial;
    }

    private void SetRandomMaterial(List<Cube> cubes, Vector3 origin)
    {
        foreach (Cube cube in cubes)
            cube.Renderer.material = _materials[Random.Range(0, _materials.Length)];

    }
}
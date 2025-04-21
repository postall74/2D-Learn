using System.Collections.Generic;
using UnityEngine;

public class CubeMaterialSetter : MonoBehaviour
{
    [SerializeField] private Material[] _materials;

    public void SetRandomMaterial(List<Cube> cubes)
    {
        foreach (Cube cube in cubes)
            cube.Renderer.material = GetRandomMaterial();
    }

    private Material GetRandomMaterial() => _materials[Random.Range(0, _materials.Length)];
}
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CubeExploder))]
public class CubeMaterialSetter : MonoBehaviour
{
    [SerializeField] private Material[] _materials;

    private void OnEnable()
    {
        GetComponent<CubeSpawner>().OnRendererSpawned += SetRandomMaterial;
    }

    private void OnDisable()
    {
        GetComponent<CubeSpawner>().OnRendererSpawned -= SetRandomMaterial;
    }

    private void SetRandomMaterial(List<Renderer> renderers)
    {
        foreach (var renderer in renderers)
            renderer.material = _materials[Random.Range(0, _materials.Length)];

    }
}
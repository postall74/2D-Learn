using System.Collections.Generic;
using UnityEngine;

public class CubeExploder: MonoBehaviour
{
    private void OnEnable()
    {
        CubeCreatedEvent.OnCubesCreated += Explode;
    }

    private void OnDisable()
    {
        CubeCreatedEvent.OnCubesCreated -= Explode;
    }

    private void Explode(List<Cube> cubes, Vector3 origin)
    {
        foreach (Cube cube in cubes)
            cube.Rigidbody.AddExplosionForce(InputConstants.DefaultExplosionForce, origin, InputConstants.DefaultExplosionRadius);
    }
}
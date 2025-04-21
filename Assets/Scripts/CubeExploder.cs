using System.Collections.Generic;
using UnityEngine;

public class CubeExploder: MonoBehaviour
{
    public void Explode(List<Cube> cubes, Vector3 origin)
    {
        foreach (Cube cube in cubes)
            cube.Rigidbody.AddExplosionForce(InputConstants.DefaultExplosionForce, origin, InputConstants.DefaultExplosionRadius);
    }
}
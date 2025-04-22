using System.Collections.Generic;
using UnityEngine;

public class CubeExploder: MonoBehaviour
{
    [SerializeField] private LayerMask _cubeLayer;

    public void Explode(List<Cube> cubes, Vector3 origin)
    {
        foreach (Cube cube in cubes)
            cube.Rigidbody.AddExplosionForce(InputConstants.DefaultExplosionForce, origin, InputConstants.DefaultExplosionRadius);
    }

    public void ExplodeSingle(Cube explodingCube)
    {
        Vector3 position = explodingCube.transform.position;
        float size = explodingCube.transform.localScale.magnitude;

        float force = InputConstants.DefaultExplosionForce / size;
        float raius = InputConstants.DefaultExplosionRadius / size;

        Collider[] hits = Physics.OverlapSphere(position, raius, _cubeLayer);

        foreach (Collider hit in hits)
        {
            if (hit.TryGetComponent(out Cube cube) && cube != explodingCube)
                cube.Rigidbody.AddExplosionForce(force, position, raius);
        }

        Destroy(explodingCube.gameObject);
    }
}
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(CubeExploder))]
public class CubeExploder: MonoBehaviour
{
    [SerializeField] private float _explosionForce = 200f;
    [SerializeField] private float _explosionRadius = 2f;

    private void OnEnable()
    {
        GetComponent<CubeSpawner>().OnRigidbodiesSpawned += Explode;
    }

    private void OnDisable()
    {
        GetComponent<CubeSpawner>().OnRigidbodiesSpawned -= Explode;
    }

    private void Explode(List<Rigidbody> rigidbodies)
    {
        foreach (var rigidbody in rigidbodies)
            rigidbody.AddExplosionForce(_explosionForce, transform.position, _explosionRadius);
    }
}

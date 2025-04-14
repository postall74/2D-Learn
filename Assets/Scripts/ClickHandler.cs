using System.Collections.Generic;
using UnityEngine;

public class ClickHandler : MonoBehaviour
{
    private const string _cubeTag = "Cube";

    [SerializeField] private GameObject _prefab;
    [SerializeField] private Material[] _materials;
    
    int _minValueCreateCube = 2;
    int _maxValueCreateCube = 7;
    float _halfSize = 0.5f;
    private float _splitChance = 1.0f;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out RaycastHit hitInfo))
            {
                GameObject hitObject = hitInfo.collider.gameObject;

                if (hitObject.CompareTag(_cubeTag))
                    SplitCube(hitObject);
            }
        }
    }

    private void SplitCube(GameObject original)
    {
        Vector3 spawnPosition = original.transform.position;
        Vector3 newScale = original.transform.localScale * _halfSize;
        List<Rigidbody> newBodies = new List<Rigidbody>();

        int spawnCount = Random.Range(_minValueCreateCube, _maxValueCreateCube);

        for (int i = 0; i < spawnCount; i++)
        {
            GameObject newCube = Instantiate(_prefab, spawnPosition, Random.rotation);
            newCube.transform.localScale = newScale;
            newCube.GetComponent<MeshRenderer>().material = _materials[Random.Range(0, _materials.Length)];

            if (!newCube.TryGetComponent(out Rigidbody rigidbody))
                rigidbody = newCube.AddComponent<Rigidbody>();

            newBodies.Add(rigidbody);
        }

        float explosionForce = 100f;
        float explosionRadius = 5f;

        foreach (var rb in newBodies)
            rb.AddExplosionForce(explosionForce, spawnPosition, explosionRadius);

        if (Random.value > _splitChance)
            Destroy(original);

        _splitChance *= 0.5f;
    }
}
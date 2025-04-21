using System.Collections.Generic;
using UnityEngine;

public class CubeSpawner : MonoBehaviour
{
    [SerializeField] private Cube _cubePrefab;

    private void OnEnable()
    {
        Cube.CubeClicked += HandlerClicked;
    }

    private void OnDisable()
    {
        Cube.CubeClicked -= HandlerClicked;
    }

    private void HandlerClicked(Cube cube, float newChance)
    {
        if (Random.value > newChance)
        {
            Destroy(cube.gameObject);
            return;
        }

        List<Cube> cubes = new List<Cube>();
        Vector3 originPosition = cube.transform.position;
        Vector3 originScale = cube.transform.localScale;
        int count = Random.Range(InputConstants.MinSpawnCount, InputConstants.MaxSpawnCount + 1);

        for (int i = 0; i < count; i++)
        {
            Cube newCube = Instantiate(_cubePrefab, originPosition, Random.rotation);
            newCube.transform.localScale = originScale * InputConstants.DefaultSplitDecay;
            newCube.Initialize(cube.SplitChance * InputConstants.DefaultSplitDecay);
            newCube.Rigidbody.useGravity = true;
            cubes.Add(newCube);
        }

        CubeCreatedEvent.Rise(cubes, originPosition);
        Destroy(cube.gameObject);
    }
}
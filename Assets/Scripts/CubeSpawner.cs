using System.Collections.Generic;
using UnityEngine;

public class CubeSpawner : MonoBehaviour
{
    [SerializeField] private Cube _cubePrefab;

    public List<Cube> Spawn(Cube cube)
    {
        int count = Random.Range(InputConstants.MinSpawnCount, InputConstants.MaxSpawnCount + 1);
        List<Cube> spawnedCubes = new(count);

        for (int i = 0; i < count; i++)
        {
            Cube newCube = InstantiateCube(cube);
            spawnedCubes.Add(newCube);
        }

        return spawnedCubes;
    }

    private Cube InstantiateCube(Cube original)
    {
        Cube cube = Instantiate(_cubePrefab, original.transform.position, Random.rotation);
        cube.transform.localScale = original.transform.localScale * InputConstants.DefaultScaleFactor;
        cube.Initialize(original.SplitChance * InputConstants.DefaultSplitDecay);
        cube.Rigidbody.useGravity = true;
        return cube;
    }
}                                                                                                                   
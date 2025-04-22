using System.Collections.Generic;
using UnityEngine;

public class GameRouter : MonoBehaviour, ICubeClickListener
{
    [SerializeField] private InputHandler _inputHandler;
    [SerializeField] private CubeSpawner _spawner;
    [SerializeField] private CubeExploder _exploder;
    [SerializeField] private CubeMaterialSetter _materialSetter;

    private void Awake()
    {
        _inputHandler.Initialize(this);
    }

    public void OnCubeClicked(Cube cube)
    {
        if (Random.value > cube.SplitChance)
        {
            _exploder.ExplodeSingle(cube);
            return;
        }

        List<Cube> spawned = _spawner.Spawn(cube);
        _materialSetter.SetRandomMaterial(spawned);
        _exploder.Explode(spawned, cube.transform.position);
        Destroy(cube.gameObject);
    }
}
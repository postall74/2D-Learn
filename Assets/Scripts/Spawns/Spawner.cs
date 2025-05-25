using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private List<SpawnPoint> _spawnPoints = new();

    private WaitForSeconds _delay;

    public event Action<Vector3, Vector3> OnSpawnRequested;

    private void Start()
    {
        _delay = new WaitForSeconds(2f);
        StartCoroutine(SpanwRoutine());
    }

    private IEnumerator SpanwRoutine()
    {
        while(enabled)
        {
            yield return _delay;
            SpawnAtRandomPoint();
        }
    }

    private void SpawnAtRandomPoint()
    {
        var index = UnityEngine.Random.Range(0, _spawnPoints.Count);
        var point = _spawnPoints[index];
        OnSpawnRequested?.Invoke(point.Position, point.Direction);
    }
}
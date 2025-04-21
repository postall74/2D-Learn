using System;
using System.Collections.Generic;
using UnityEngine;

public static class CubeCreatedEvent
{
    public static event Action<List<Cube>, Vector3> OnCubesCreated;

    public static void Rise(List<Cube> cubes, Vector3 origin)
    {
        OnCubesCreated?.Invoke(cubes, origin);
    }
}
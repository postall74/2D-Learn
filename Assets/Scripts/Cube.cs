using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(Renderer))]
public class Cube : MonoBehaviour
{
    [SerializeField, Range(InputConstants.MinSplitChance, InputConstants.MaxSplitChance)] private float _splitChance = 1f;

    public float SplitChance => _splitChance;
    public Rigidbody Rigidbody { get; private set; }
    public Renderer Renderer { get; private set; }

    private void Awake()
    {
        Rigidbody = GetComponent<Rigidbody>();
        Renderer = GetComponent<Renderer>();
    }

    public void Initialize(float splitChance)
    {
        _splitChance = splitChance;
    }
}
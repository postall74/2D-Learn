using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(Renderer))]
public class Cube : MonoBehaviour
{
    [SerializeField, Range(InputConstants.MinSplitChance, InputConstants.MaxSplitChance)] private float _splitChance = 1f;

    private Rigidbody _rigidbody;
    private Renderer _renderer;

    public static event Action<Cube, float> CubeClicked;

    public float SplitChance => _splitChance;
    public Rigidbody Rigidbody => _rigidbody;
    public Renderer Renderer => _renderer;
    
    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _renderer = GetComponent<Renderer>();
    }

    public void Initialize(float splitChance)
    {
        _splitChance = splitChance;
    }    

    public void Click()
    {
        CubeClicked?.Invoke(this, _splitChance);
    }
}

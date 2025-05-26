using System;
using UnityEngine;

[Serializable]
public class Movement : IMovement
{
    private Transform _transform;
    private float _speed;
    private Vector3 _direction;

    public void Initialize(Transform transform) =>
        _transform = transform;

    public void SetSpeed(float speed) => 
        _speed = speed;

    public void Move(Vector3 direction) =>
        _direction = direction.normalized;

    public void UpdateMovement()
    {
        if (_transform == null) 
            return;
        _transform.rotation = Quaternion.identity;
        _transform.position += _direction * _speed * Time.deltaTime * Settings.HalfSpeed;
    }

    public void Stop() =>
        _direction = Vector3.zero;
}
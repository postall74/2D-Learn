using System;
using UnityEngine;

[Serializable]
public class Mover : IMover
{
    private Transform _transform;
    private Transform _target;
    private float _speed;
    private Vector3 _direction;

    public void Initialize(Transform transform) =>
        _transform = transform;

    public void SetTarget(Transform target) => 
        _target = target;

    public void SetSpeed(float speed) => 
        _speed = speed;

    public void NormalizeDirection(Vector3 direction) =>
        _direction = direction.normalized;

    public void Move()
    {
        if (_transform == null || _target == null) 
            return;

        _direction = (_target.position - _transform.position).normalized;
        _transform.position += _direction * (_speed * Time.deltaTime);
        _transform.rotation = Quaternion.LookRotation(_direction);
    }

    public void Stop() =>
        _direction = Vector3.zero;
}
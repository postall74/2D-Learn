using System;
using UnityEngine;

[Serializable]
public class Mover : IMover
{
    private Transform _transform;
    private Transform _target;
    private float _speed;

    public void Initialize(Transform transform, Transform target, float speed)
    {
        _transform = transform;
        _target = target;
        _speed = speed;
    }

    public void UpdateMovement()
    {
        if (_transform == null || _target == null)
            return;

        Vector3 direction = (_target.position - _transform.position).normalized;
        _transform.position += direction * (_speed * Time.deltaTime);
        _transform.rotation = Quaternion.LookRotation(direction);
    }

    public void Stop() { }
}
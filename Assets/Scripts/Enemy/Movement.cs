using UnityEngine;

public class Movement : MonoBehaviour, IMovement
{
    private Transform _transform;
    private Rigidbody _rigidbody;
    private float _speed;

    public void Initialize(float maxSpeed)
    {
        _speed = Random.Range(1f, maxSpeed);
    }

    public void Move(Vector3 direction)
    {
        transform.position += direction * Time.deltaTime * _speed;
    }

    public void Stop()
    {
        _rigidbody.velocity = Vector3.zero;
    }
}
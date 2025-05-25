using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(Animator), typeof(Collider))]
public class Enemy : MonoBehaviour, IEnemy
{
    [SerializeField] private float _maxSpeed;

    private IMovement _movement;
    private IAnimation _animation;
    private Animator _animator;

    public event Action<Enemy> OnActivated;
    public event Action<Enemy> OnDeactivated;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bounce"))
            Deactivate();
    }

    private void OnDestroy()
    {
        OnDeactivated = null;
    }

    public void Initialize(IMovement movement, IAnimation animation)
    {
        _movement = movement;
        _animation = animation;

        _movement.Initialize(_maxSpeed);
        _animation.Initialize(_animator);
    }

    public void Activate(Vector3 position, Vector3 direction)
    {
        transform.position = position;
        gameObject.SetActive(true);

        float speed = UnityEngine.Random.Range(1f, _maxSpeed);
        _movement.Move(direction);
        _animation.Play(speed);
        OnActivated?.Invoke(this);
    }

    public void Deactivate()
    {
        _movement.Stop();
        _animation.Stop();
        gameObject.SetActive(false);
        OnDeactivated?.Invoke(this);
    }
}
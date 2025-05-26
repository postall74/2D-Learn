using System;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Enemy : MonoBehaviour, IEnemy
{
    private IMovement _movement;
    private IAnimation _animation;
    private Animator _animator;

    public event Action<Enemy> OnDeactivated;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        (_movement as Movement)?.UpdateMovement();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Settings.BounceTag))
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

        _movement.Initialize(transform);
        _animation.Initialize(_animator);
    }

    public void Activate(Vector3 position, Vector3 direction)
    {
        float speed = UnityEngine.Random.Range(Settings.MinSpeed, Settings.MaxSpeed);
        
        transform.rotation = Quaternion.LookRotation(direction);
        transform.position = position;
        gameObject.SetActive(true);

        _movement.SetSpeed(speed);
        _movement.Move(direction);
        _animation.Play(speed);
    }

    public void Deactivate()
    {
        _movement.Stop();
        _animation.Stop();
        gameObject.SetActive(false);
        OnDeactivated?.Invoke(this);
    }
}
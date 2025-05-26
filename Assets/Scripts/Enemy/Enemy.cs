using System;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Enemy : MonoBehaviour, IEnemy
{
    private IMover _mover;
    private IAnimation _animation;
    private Animator _animator;

    public event Action<Enemy> OnDeactivated;

    private void Awake()
    {
        _animator = GetComponent<Animator>();

        if (_mover != null || _animation != null)
            return;

        _mover = new Mover();
        _animation = new Animation();

        _mover.Initialize(transform);
        _animation.Initialize(_animator);
    }

    private void FixedUpdate() =>
        _mover.Move();

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent<Boundary>(out _))
            Deactivate();
    }

    private void OnDestroy() =>
        OnDeactivated = null;

    public void Activate(Vector3 position, Vector3 direction)
    {
        float speed = UnityEngine.Random.Range(Settings.MinSpeed, Settings.MaxSpeed);

        transform.SetPositionAndRotation(position, Quaternion.LookRotation(direction));
        gameObject.SetActive(true);

        _mover.SetSpeed(speed);
        _mover.NormalizeDirection(direction);
        _animation.Play(speed);
    }

    public void Deactivate()
    {
        _mover.Stop();
        _animation.Stop();
        gameObject.SetActive(false);
        OnDeactivated?.Invoke(this);
    }
}
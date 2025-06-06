using System;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public abstract class EnemyBase : MonoBehaviour, IEnemy
{
    public event Action<EnemyBase> Deactivated;

    protected IMover _mover;
    protected IAnimation _animation;
    protected Animator _animator;

    public abstract EnemyType Type { get; }

    protected virtual void Awake()
    {
        _animator = GetComponent<Animator>();
        _mover = new Mover();
        _animation = new Animation();
        _animation.Initialize(_animator);
    }

    protected virtual void Update()
    {
        _mover.UpdateMovement();
    }

    protected virtual void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Boundary>(out _))
            Deactivate();
    }

    protected abstract float GetSpeed();

    public void Initialize(Transform target)
    {
        if (_mover == null || _animation == null)
        {
#if UNITY_EDITOR
            Debug.LogError("Компоненты Mover и Animation не инициализированы, проверь их!");
#endif
            return;
        }

        float speed = GetSpeed();
        _mover.Initialize(transform, target, speed);
        _animation.Play(speed);
    }

    public void Activate(Vector3 position)
    {
        transform.position = position;
        gameObject.SetActive(true);
    }

    public void Deactivate()
    {
        _mover.Stop();
        _animation.Stop();
        gameObject.SetActive(false);
        Deactivated?.Invoke(this);
    }
}
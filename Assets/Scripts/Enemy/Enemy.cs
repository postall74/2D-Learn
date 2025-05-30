using System;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Enemy : MonoBehaviour, IEnemy
{
    [SerializeField] private EnemyType _type;

    private IMover _mover;
    private IAnimation _animation;
    private Animator _animator;

    public EnemyType Type => _type;
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

    private void Update() =>
        _mover.Move();

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent<Boundary>(out _))
            Deactivate();
    }

    private void OnDestroy() =>
        OnDeactivated = null;

    public void Initialize(EnemyType type, Transform target)
    {
        _type = type;
        SetTarget(target);
    }

    public void Activate(Vector3 position)
    {
        float speed = GetSpeedByType(_type);

        transform.position = position;
        gameObject.SetActive(true);

        _mover.SetSpeed(speed);
        _animation.Play(speed); ;
    }

    public void SetTarget(Transform target) =>
        _mover.SetTarget(target);

    private float GetSpeedByType(EnemyType type)
    {
        return type switch
        {
            EnemyType.Basic => UnityEngine.Random.Range(Settings.BasicMinSpeed, Settings.BasicMaxSpeed),
            EnemyType.Fast => UnityEngine.Random.Range(Settings.FastMinSpeed, Settings.FastMaxSpeed),
            EnemyType.Slow => UnityEngine.Random.Range(Settings.SlowMinSpeed, Settings.SlowMaxSpeed),
            _ => Settings.DefaultEnemySpeed
        };
    }

    public void Deactivate()
    {
        _mover.Stop();
        _animation.Stop();
        gameObject.SetActive(false);
        OnDeactivated?.Invoke(this);
    }
}
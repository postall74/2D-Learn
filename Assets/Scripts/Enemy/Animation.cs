using System;
using UnityEngine;

[Serializable]
public class Animation : IAnimation
{
    private Animator _animator;
    private int _speedHash = Animator.StringToHash("Speed");

    public void Initialize(Animator animator)
    {
        _animator = animator;
    }

    public void Play(float speed)
    {
        _animator.SetFloat(_speedHash, speed);
        _animator.enabled = true;
    }

    public void Stop()
    {
        _animator.enabled = false;
    }
}

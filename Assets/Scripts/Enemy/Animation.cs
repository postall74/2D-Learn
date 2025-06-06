using System;
using UnityEngine;

[Serializable]
public class Animation : IAnimation
{
    private Animator _animator;

    public void Initialize(Animator animator) =>
        _animator = animator;

    public void Play(float speed)
    {
        if (_animator == null) 
            return;

        _animator.SetFloat(Settings.SpeedHash, speed);
        _animator.enabled = true;
    }

    public void Stop() 
    {
        if (_animator == null) 
            return;

        _animator.enabled = false;
    }
}
using UnityEngine;

public interface IAnimation
{
    public void Initialize(Animator animator);
    public void Play(float speed);
    public void Stop();
}

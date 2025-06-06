using UnityEngine;

public interface IMover
{
    public void Initialize(Transform transform, Transform target, float speed);
    void UpdateMovement();
    public void Stop();
}
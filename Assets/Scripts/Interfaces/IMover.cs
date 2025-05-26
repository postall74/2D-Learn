using UnityEngine;

public interface IMover
{
    public void Initialize(Transform transform);
    public void SetSpeed(float maxSpeed);
    public void NormalizeDirection(Vector3 direction);
    public void Move();
    public void Stop();
}
using UnityEngine;

public interface IMovement
{
    public void Initialize(Transform transform);
    public void SetSpeed(float maxSpeed);
    public void Move(Vector3 direction);
    public void Stop();
}
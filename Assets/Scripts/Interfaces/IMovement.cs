using UnityEngine;

public interface IMovement
{
    public void Initialize(float _speed);
    public void Move(Vector3 direction);
    public void Stop();
}

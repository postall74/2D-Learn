using UnityEngine;

public interface IEnemy
{
    public void Initialize(IMovement movement, IAnimation animation);
    public void Activate(Vector3 position, Vector3 direction);
    public void Deactivate();
}

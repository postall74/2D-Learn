using UnityEngine;

public interface IEnemy
{
    public void Initialize(Transform target);
    public void Activate(Vector3 position);
    public void Deactivate();
}
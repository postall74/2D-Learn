using UnityEngine;

public interface IEnemy
{
    public void Initialize(EnemyType type, Transform target);
    public void Activate(Vector3 position);
    public void Deactivate();
}
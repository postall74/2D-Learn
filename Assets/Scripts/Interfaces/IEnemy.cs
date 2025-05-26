using UnityEngine;

public interface IEnemy
{
    public void Activate(Vector3 position, Vector3 direction);
    public void Deactivate();
}
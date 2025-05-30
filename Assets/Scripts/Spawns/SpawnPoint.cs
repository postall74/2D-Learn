using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    [SerializeField] private EnemyType _type;
    [SerializeField] private Target _target;

    public Vector3 Position => transform.position;
    public EnemyType Type => _type;
    public Target Target => _target;
}
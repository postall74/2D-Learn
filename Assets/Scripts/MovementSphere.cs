using UnityEngine;

public class MovementSphere : MonoBehaviour
{
    [SerializeField] private Transform _transform;
    [SerializeField] private float _speed = 0.5f;

    private void Start()
    {
        if (_transform == null)
            _transform = GetComponent<Transform>();
    }

    private void Update()
    {
        MoveForward(_transform, _speed);
    }

    private void MoveForward(Transform transform, float speed)
    {
        transform.transform.Translate(speed * Time.deltaTime * Vector3.forward);
    }
}
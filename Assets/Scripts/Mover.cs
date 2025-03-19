using UnityEngine;

public class Mover : MonoBehaviour
{
    [SerializeField] private float _speed = 0.5f;
    private Transform _transform;

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
        transform.transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }
}
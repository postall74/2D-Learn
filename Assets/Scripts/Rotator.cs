using UnityEngine;

public class Rotator : MonoBehaviour
{
    private Transform _transform;
    [SerializeField] private float _rotationSpeed = 50.0f;

    private void Start()
    {
        if (_transform == null)
            _transform = GetComponent<Transform>();
    }

    private void Update()
    {
        RotateAxisY(_transform, _rotationSpeed);
    }

    private void RotateAxisY(Transform transform, float rotateSpeed)
    {
        transform.transform.Rotate(Vector3.up * rotateSpeed * Time.deltaTime);
    }
}

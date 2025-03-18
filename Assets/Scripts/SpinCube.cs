using UnityEngine;

[RequireComponent(typeof(BoxCollider), typeof(MeshRenderer))]
public class SpinCube : MonoBehaviour
{
    [SerializeField] private Transform _transform;
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
        transform.transform.Rotate(rotateSpeed * Time.deltaTime * Vector3.up);
    }
}

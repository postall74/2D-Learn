using UnityEngine;

[RequireComponent(typeof(BoxCollider), typeof(MeshRenderer))]
public class MovementSpinGrowthCube : MonoBehaviour
{
    [SerializeField] private Transform _transform;
    [SerializeField] private float _moveSpeed = 2f;
    [SerializeField] private float _rotationSpeed = 25.0f;
    [SerializeField] private float _growSpeed = 0.2f;

    private void Start()
    {
        if (_transform == null)
            _transform = GetComponent<Transform>();
    }

    private void Update()
    {
        MoveForward(_transform, _moveSpeed);
        ScaleCapsule(_transform, _growSpeed);
        RotateAxisY(_transform, _rotationSpeed);
    }

    private void MoveForward(Transform transform, float speed)
    {
        transform.transform.Translate(speed * Time.deltaTime * Vector3.forward);
    }

    private void ScaleCapsule(Transform transform, float growSpeed)
    {
        transform.transform.localScale += growSpeed * Time.deltaTime * Vector3.one;
    }

    private void RotateAxisY(Transform transform, float rotateSpeed)
    {
        transform.transform.Rotate(rotateSpeed * Time.deltaTime * Vector3.up);
    }
}

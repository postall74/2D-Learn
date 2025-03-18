using UnityEngine;

public class GrowthCapsule : MonoBehaviour
{
    [SerializeField] private Transform _transform;
    [SerializeField] private float _growSpeed = 0.5f;

    private void Start()
    {
        if (_transform == null)
            _transform = GetComponent<Transform>();
    }

    private void Update()
    {
        ScaleCapsule(_transform, _growSpeed);
    }

    private void ScaleCapsule(Transform transform, float growSpeed)
    {
        transform.transform.localScale += growSpeed * Time.deltaTime * Vector3.one;
    }
}

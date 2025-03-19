using UnityEngine;

public class Scaler : MonoBehaviour
{
    [SerializeField] private float _growSpeed = 0.5f;
    private Transform _transform;

    private void Start()
    {
        if (_transform == null)
            _transform = GetComponent<Transform>();
    }

    private void Update()
    {
        ScaleUp(_transform, _growSpeed);
    }

    private void ScaleUp(Transform transform, float growSpeed)
    {
        transform.transform.localScale += Vector3.one * growSpeed * Time.deltaTime;
    }
}

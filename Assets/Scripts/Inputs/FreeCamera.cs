using UnityEngine;

public class FreeCamera : MonoBehaviour
{
    [SerializeField] private Camera _mainCamera;

    public Camera MainCamera => _mainCamera;

    private void Awake()
    {
        if (_mainCamera == null)
            _mainCamera = Camera.main;
    }

    public void Move(Vector3 direction, float speed)
    {
        transform.position += transform.TransformDirection(direction) * speed * Time.deltaTime;
    }

    public void Look(float yawDelta, float pitchDelta)
    {
        Vector3 angles = transform.eulerAngles;
        float currentPitch = NormalizeAngle(angles.x);
        float pitch = currentPitch - pitchDelta;
        float yaw = angles.y + yawDelta;
        pitch = Mathf.Clamp(pitch, InputConstants.LookClampMin, InputConstants.LookClampMax);
        transform.rotation = Quaternion.Euler(pitch, yaw, 0);
    }

    public void ClampPosition(Vector3 min, Vector3 max)
    {
        Vector3 position = transform.position;
        position.x = Mathf.Clamp(position.x, min.x, max.x);
        position.y = Mathf.Clamp(position.y, min.y, max.y);
        position.z = Mathf.Clamp(position.z, min.z, max.z);
        transform.position = position;
    }

    private float NormalizeAngle(float angle)
    {
        angle %= 360;

        if (angle > 180)
            angle -= 360;

        return angle; 
    }
}
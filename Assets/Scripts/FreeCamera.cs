using UnityEngine;

public class FreeCamera : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 10f;
    [SerializeField] private float mouseSensitivity = 2f;
    [SerializeField] private Vector3 minBounds = new Vector3(-9, 1, -9);
    [SerializeField] private Vector3 maxBounds = new Vector3(9, 9, 9);

    private float yaw;
    private float pitch;

    private void Update()
    {
        HandleMouseLook();
        HandleMovement();
        ClampPosition();
    }

    private void HandleMouseLook()
    {
        if (Input.GetMouseButton(1)) // œ Ã
        {
            yaw += Input.GetAxis("Mouse X") * mouseSensitivity;
            pitch -= Input.GetAxis("Mouse Y") * mouseSensitivity;
            pitch = Mathf.Clamp(pitch, -90f, 90f);

            transform.eulerAngles = new Vector3(pitch, yaw, 0f);
        }
    }

    private void HandleMovement()
    {
        Vector3 move = new Vector3(
            Input.GetAxis("Horizontal"),
            Input.GetKey(KeyCode.Space) ? 1 : Input.GetKey(KeyCode.LeftControl) ? -1 : 0,
            Input.GetAxis("Vertical")
        );

        transform.position += transform.TransformDirection(move) * moveSpeed * Time.deltaTime;
    }

    private void ClampPosition()
    {
        Vector3 pos = transform.position;
        pos.x = Mathf.Clamp(pos.x, minBounds.x, maxBounds.x);
        pos.y = Mathf.Clamp(pos.y, minBounds.y, maxBounds.y);
        pos.z = Mathf.Clamp(pos.z, minBounds.z, maxBounds.z);
        transform.position = pos;
    }
}

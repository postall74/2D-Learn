using UnityEngine;

public class InputHandler : MonoBehaviour
{
    [SerializeField] private FreeCamera _camera;
    [SerializeField] private float _moveSpeed = 10f;
    [SerializeField] private float _lookSensitivity = 2f;
    [SerializeField] private Vector3 _minBounds = new Vector3(-50, 1, -50);
    [SerializeField] private Vector3 _maxBounds = new Vector3(50, 10, 50);

    private void Awake()
    {
        if (_camera == null)
            _camera = FindFirstObjectByType<FreeCamera>();
    }

    private void Update()
    {
        HandleClick();
        HandleCameraMovement();
    }

    private void HandleClick()
    {
        if (Input.GetMouseButtonDown(InputConstants.LeftMouseButton))
        {
            Ray ray = _camera.MainCamera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out RaycastHit hit) && hit.collider.TryGetComponent(out Cube clickable))
                clickable.Click();
        }
    }

    private void HandleCameraMovement()
    {
        if (Input.GetMouseButton(InputConstants.RightMouseButton))
        {
            float yawDelta = Input.GetAxis(InputConstants.MouseX) * _lookSensitivity;
            float pitchDelta = Input.GetAxis(InputConstants.MouseY) * _lookSensitivity;
            _camera.Look(yawDelta, pitchDelta);
        }

        Vector3 direction = new Vector3(
            Input.GetAxis(InputConstants.Horizontal),
            Input.GetKey(InputConstants.MoveUp) ? 1 : Input.GetKey(InputConstants.MoveDown) ? -1 : 0,
            Input.GetAxis(InputConstants.Vertical)
            );

        _camera.Move(direction, _moveSpeed);
        _camera.ClampPosition(_minBounds, _maxBounds);
    }
}
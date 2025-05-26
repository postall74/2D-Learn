using UnityEngine;

[RequireComponent(typeof(FreeCamera))]
public class CameraController : MonoBehaviour
{
    [SerializeField] private InputReader _inputReader;
    [SerializeField] private float _moveSpeed = 10f;
    [SerializeField] private float _lookSensitivity = 2f;
    [SerializeField] private Vector3 _minBounds = new Vector3(-50, 1, -50);
    [SerializeField] private Vector3 _maxBounds = new Vector3(50, 10, 50);

    private FreeCamera _freeCamera;

    private void Awake()
    {
        _freeCamera = GetComponent<FreeCamera>();
        ValidateDependencies();
    }

    private void Update()
    {
        HandleCameraMovement();
        HandleCameraLook();
    }

    private void HandleCameraMovement()
    {
        _freeCamera.Move(_inputReader.MoveDirection, _moveSpeed);
        _freeCamera.ClampPosition(_minBounds, _maxBounds);
    }

    private void HandleCameraLook()
    {
        if (_inputReader.IsRightMouseButtonPressed)
        {
            Vector2 lookDelta = _inputReader.LookDelta * _lookSensitivity;
            _freeCamera.Look(lookDelta.x, lookDelta.y);
        }
    }

    private void ValidateDependencies()
    {
        if (_inputReader == null)
            _inputReader = FindFirstObjectByType<InputReader>();

        if (_freeCamera == null)
            _freeCamera = FindFirstObjectByType<FreeCamera>();
    }
}
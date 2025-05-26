using UnityEngine;

public class InputReader : MonoBehaviour
{
    public Vector3 MoveDirection { get; private set; }
    public Vector2 LookDelta { get; private set; }
    public bool IsRightMouseButtonPressed { get; private set; }

    private void Update()
    {
        ReadMovementInput();
        ReadLookInput();
        ReadMouseButton();
    }

    private void ReadMovementInput()
    {
        MoveDirection = new Vector3
            (
                Input.GetAxis(InputConstants.Horizontal),
                Input.GetKey(InputConstants.MoveUp) ? 1 : Input.GetKey(InputConstants.MoveDown) ? -1 : 0,
                Input.GetAxis(InputConstants.Vertical)
            );
    }

    private void ReadLookInput()
    {
        LookDelta = new Vector2
            (
                Input.GetAxis(InputConstants.MouseX),
                Input.GetAxis(InputConstants.MouseY)
            );
    }

    private void ReadMouseButton()
    {
        IsRightMouseButtonPressed = Input.GetMouseButton(InputConstants.RightMouseButton);
    }
}
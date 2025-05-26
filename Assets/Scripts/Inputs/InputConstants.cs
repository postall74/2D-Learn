using UnityEngine;

public static class InputConstants
{
    public const string MouseX = "Mouse X";
    public const string MouseY = "Mouse Y";

    public const string Horizontal = "Horizontal";
    public const string Vertical = "Vertical";

    public const KeyCode MoveUp = KeyCode.Space;
    public const KeyCode MoveDown = KeyCode.LeftControl;

    public const int LeftMouseButton = 0;
    public const int RightMouseButton = 1;

    public const float LookClampMin = -90f;
    public const float LookClampMax = 90f;
}
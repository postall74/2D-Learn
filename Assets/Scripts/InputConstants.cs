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

    public const float DefaultSplitChance = 1f;
    public const float MinSplitChance = 0f;
    public const float MaxSplitChance = 1f;

    public const float DefaultExplosionForce = 200f;
    public const float DefaultExplosionRadius = 2f;

    public const int MinSpawnCount = 2;
    public const int MaxSpawnCount = 6;

    public const float DefaultSplitDecay = 0.5f;
    public const float DefaultScaleFactor = 0.5f;

    public const float LookClampMin = -90f;
    public const float LookClampMax = 90f;
}
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

    public const int MinMaterialCount = 2;

    public const float LookClampMin = -90f;
    public const float LookClampMax = 90f;

    public const string PlatformTag = "Platform";

    public const float MinLifeTimeCube = 2f;
    public const float MaxLifeTimeCube = 5f;

    public const float MinSpawnTimeCube = 0.0001f;
    public const float MaxSpawnTimeCube = 0.001f;

    public const string StandatrMaterial = "Standart";

    public const float BasePlaneSize = 10f; 
    public const float SpawnHeightOffset = 15f;

    public const float DefaultBounciness = 0.2f;
    public const float MinBounciness = 0f;
    public const float MaxBounciness = 1f;

    public const float MinSoundVolume = 0f;
    public const float MaxSoundVolume = 1f;
    public const float DefaultSoundVolume = 0.5f;
}
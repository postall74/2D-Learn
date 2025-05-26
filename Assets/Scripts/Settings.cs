using UnityEngine;

public static class Settings
{
    public const string SpeedParamName = "Speed";
    public static readonly int SpeedHash = Animator.StringToHash(SpeedParamName);
    public const float SpawnInterval = 2f;
    public const float MinSpeed = 1f;
    public const float MaxSpeed = 10f;
    public const string BounceTag = "Bounce";
    public const int DefaultPoolCapacity = 10;
    public const int MaxPoolSize = 100;
    public const float HalfSpeed = 0.5f;
}
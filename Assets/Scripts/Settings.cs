using UnityEngine;

public static class Settings
{
    public const string SpeedParamName = "Speed";
    public static readonly int SpeedHash = Animator.StringToHash(SpeedParamName);
    public const float SpawnInterval = 2f;
    public const int DefaultPoolCapacity = 50;
    public const int MaxPoolSize = 100;
    public const float BasicMinSpeed = 3f;
    public const float BasicMaxSpeed = 6f;
    public const float FastMinSpeed = 6f;
    public const float FastMaxSpeed = 9f;
    public const float SlowMinSpeed = 1f;
    public const float SlowMaxSpeed = 3f;
    public const float DefaultEnemySpeed = 5f;
}
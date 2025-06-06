using UnityEngine;

public class SlowEnemy : EnemyBase
{
    public override EnemyType Type => EnemyType.Slow;

    protected override float GetSpeed() =>
        UnityEngine.Random.Range(Settings.SlowMinSpeed, Settings.SlowMaxSpeed);
}
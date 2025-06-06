using UnityEngine;

public class FastEnemy : EnemyBase
{
    public override EnemyType Type => EnemyType.Fast;

    protected override float GetSpeed() =>
        UnityEngine.Random.Range(Settings.FastMinSpeed, Settings.FastMaxSpeed);
}

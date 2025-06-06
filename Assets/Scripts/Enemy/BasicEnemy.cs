using UnityEngine;

public class BasicEnemy : EnemyBase
{
    public override EnemyType Type => EnemyType.Basic;

    protected override float GetSpeed() =>
        UnityEngine.Random.Range(Settings.BasicMinSpeed, Settings.BasicMaxSpeed);
}

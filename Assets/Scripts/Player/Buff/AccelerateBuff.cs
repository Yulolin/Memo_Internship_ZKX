using UnityEngine;

public class AccelerateBuff : Buff
{
    private float speedRatio = 1.5f;
    public AccelerateBuff(PlayerController player) : base(player)
    {
        speedRatio = player.SpeedRatio;
        buffType = BuffType.Accelerate;
    }

    private float originSpeed;
    public override void Apply()
    {
        originSpeed = player.moveSpeed;
        player.moveSpeed*= speedRatio;
    }

    public override void Remove()
    {
        player.moveSpeed = originSpeed;
    }
}

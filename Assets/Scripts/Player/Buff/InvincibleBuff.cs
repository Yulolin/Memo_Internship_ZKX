
using UnityEngine;

public class InvincibleBuff : Buff
{
    public InvincibleBuff(PlayerController player) : base(player)
    {
        buffType = BuffType.Invincible;
    }

    public override void Apply()
    {
        player.GetComponent<SpriteRenderer>().color = new Color(1,1,1,0.9f);
    }
    public override void Update()
    {
        base.Update();
        player.isInvincible = true;
    }

    public override void Remove()
    {
        player.isInvincible = false;
        player.GetComponent<SpriteRenderer>().color = new Color(1,1,1,1);
    }
}


using UnityEngine;

public class FlyBuff : Buff
{
    public float flySpeed;
    private Rigidbody2D playerRB;
    
    public FlyBuff(PlayerController player) : base(player)
    {
        flySpeed = player.FlySpeed;
        playerRB =  player.GetComponent<Rigidbody2D>();
        buffType = BuffType.Fly;
    }

    public override void Apply()
    {
        
    }
    public override void Update()
    {
        base.Update();
        if (playerRB.velocity.y < 0 && !player.isGrounded)
        {
            player.ChangeState(player.flyState);
        }
    }

    public override void Remove()
    {
        player.ChangeState(player.idleState);
    }
}


using UnityEngine;

public class FlyState :State
{
    private Rigidbody2D playerRB;
    private FlyBuff flyBuff;
    public FlyState(PlayerController player,FlyBuff buff) : base(player)
    {
        playerRB =  player.GetComponent<Rigidbody2D>();
        flyBuff = buff;
    }
    
    public override void Enter()
    {
        Debug.Log("进入Flying");
        player.animator.SetBool("Fly",true);
    }

    public override void Update()
    {
        // 飞行的时候可以移动
        player.Move(false);
        player.Attack();
        // player.Jump();
        if (Input.GetKey(KeyCode.W))
        {
            playerRB.velocity = new Vector2(playerRB.velocity.x,flyBuff.flySpeed);
        }
        if (player.isGrounded&&!Input.GetKey(KeyCode.W))
        {
            player.ChangeState(player.idleState);
        }
    }

    public override void Exit()
    {
        player.animator.SetBool("Fly",false);
        Debug.Log("退出Flying");
    }
}

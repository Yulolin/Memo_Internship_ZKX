using UnityEngine;

public class JumpState : State
{
    private Rigidbody2D playerRB;
    public JumpState(PlayerController player) : base(player)
    {
        playerRB =  player.GetComponent<Rigidbody2D>();
    }
    
    public override void Enter()
    {
        Debug.Log("进入Jump");
    }

    public override void Update()
    {
        // 跳跃的时候可以移动
        player.Move(false);
        player.Attack();
        // player.Jump();
        if (playerRB.velocity.y > 1e-6)
        {
            player.animator.SetBool("Up",true);
            player.animator.SetBool("Down",false);
        }
        else if(playerRB.velocity.y < -1e-6)
        {
            player.animator.SetBool("Up",false);
            player.animator.SetBool("Down",true);
        }
    }

    public override void Exit()
    {
        Debug.Log("退出Jump");
        player.animator.SetBool("Up",false);
        player.animator.SetBool("Down",false);
    }
}
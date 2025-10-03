using UnityEngine;

public class JumpState : State
{
    public JumpState(PlayerController player) : base(player)
    {
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
        player.Jump();
    }

    public override void Exit()
    {
        Debug.Log("退出Jump");
    }
}
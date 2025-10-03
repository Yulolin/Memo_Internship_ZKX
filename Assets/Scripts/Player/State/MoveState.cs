using UnityEngine;

public class MoveState : State
{
    public MoveState(PlayerController player) : base(player)
    {
    }

    public override void Enter()
    {
        Debug.Log("进入Move");
    }

    public override void Update()
    {
        player.Move(true);
        // 移动时可以跳跃
        player.Jump();
        player.Attack();
    }

    public override void Exit()
    {
        Debug.Log("退出Move");
    }
}
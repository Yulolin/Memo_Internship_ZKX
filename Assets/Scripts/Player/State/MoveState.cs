using UnityEngine;

public class MoveState : State
{
    // private Transform playerTransform;
    public MoveState(PlayerController player) : base(player)
    {
        // playerTransform = player.transform;
    }

    public override void Enter()
    {
        player.animator.SetBool("Run", true);
        Debug.Log("进入Move");
    }

    public override void Update()
    {
        player.Move(true);
        if (Mathf.Abs(player.GetComponent<Rigidbody2D>().velocity.x) < 1e-6)
        {
            player.ChangeState(player.idleState);
        }
        // 移动时可以跳跃
        player.Jump();
        player.Attack();
    }

    public override void Exit()
    {
        Debug.Log("退出Move");
        player.animator.SetBool("Run", false);
    }
}
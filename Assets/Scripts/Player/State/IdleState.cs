

using UnityEngine;

public class IdleState : State
{
    public IdleState(PlayerController player) : base(player)
    {
    }

    
    public override void Enter()
    {
        Debug.Log("进入Idle");        
    }
    
    public override void Update()
    {
        player.Move(true);
        player.Jump();
        player.Attack();
    }

    public override void Exit()
    {
        Debug.Log("退出Idle");        
    }
}

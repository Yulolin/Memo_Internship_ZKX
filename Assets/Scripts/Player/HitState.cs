using UnityEngine;

public class HitState : State
{
    public HitState(PlayerController player) : base(player)
    {
    }

    public override void Enter()
    {
        Debug.Log("进入Hit");
    }

    public override void Update()
    {
        
    }

    public override void Exit()
    {
        Debug.Log("退出Hit");
    }
}
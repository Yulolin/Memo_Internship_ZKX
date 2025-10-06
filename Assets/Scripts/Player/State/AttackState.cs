using UnityEngine;

public class AttackState : State
{
    public AttackState(PlayerController player) : base(player)
    {
    }

    public override void Enter()
    {
        Debug.Log("进入Attack");
        player.animator.SetTrigger("Attack");
    }

    private float timeCount = 0;
    private bool createdArrow = false;
    public override void Update()
    {
        timeCount += Time.deltaTime;
        if (timeCount >= player.arrowDelay && createdArrow == false)
        {
            createdArrow = true;
            player.CreateArrow();
        }

        if (timeCount >= player.attackDuration)
        {
            player.ChangeState(player.idleState);
        }
    }

    public override void Exit()
    {
        Debug.Log("退出Attack");
        timeCount = 0;
        createdArrow = false;
    }
}
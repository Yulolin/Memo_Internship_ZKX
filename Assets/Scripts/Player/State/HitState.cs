using System.Threading.Tasks;
using NodeCanvas.Tasks.Actions;
using UnityEngine;

public class HitState : State
{
    public float invincibleTime = 3f;
    private float t = 0;
    public HitState(PlayerController player) : base(player)
    {
        invincibleTime = player.invincibleTime;
    }

    public override void Enter()
    {
        Debug.Log("进入Hit");
        PlayerManager.Instance.ChangeHP(-1);
        t = 0;
        player.animator.SetBool("Hit", true);
        player.animator.SetTrigger("StartHit");
        player.audioSource.clip = AudioManager.Instance.PlayerHit;
        player.audioSource.Play();
        SetInvincible();
        // player
    }

    public override void Update()
    {
        player.Move(true);
        player.Jump();
        t+=Time.deltaTime;
        if(t>invincibleTime)
        {
            player.ChangeState(player.idleState);
        }
    }
    public override void Exit()
    {
        Debug.Log("退出Hit");
        player.animator.SetBool("Hit", false);
    }
    // 确保任务不会重复启动
    private bool isInvincibleTaskRunning = false;
    async Task SetInvincible()
    {
        player.isInvincible = true;
        if (isInvincibleTaskRunning) return;  // 防止重复启动任务
        isInvincibleTaskRunning = true;
        // 播放闪烁

        player.StartACoroutine(
            SpriteBlinkUtil.Blinking(player.GetComponent<SpriteRenderer>(), invincibleTime));
        await Task.Delay((int)(invincibleTime * 1000));
        player.isInvincible = false;
        isInvincibleTaskRunning = false;
    }

}
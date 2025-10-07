using UnityEngine;

public enum BuffType
{
    none,
    HP,
    Fly,
    Accelerate,
    Invincible
}
public abstract class Buff
{
    protected PlayerController player;
    protected float BuffTimeCount = 0;
    protected BuffType buffType;

    public Buff(PlayerController player)
    {
        this.player = player;
    }

    public abstract void Apply();

    public virtual void Update()
    {
        BuffTimeCount += Time.deltaTime;
        if (BuffTimeCount > player.BuffTime)
        {
            player.ChangeBuff(BuffType.none);
        }
    }
    public abstract void Remove();

}
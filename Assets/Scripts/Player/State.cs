using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class State
{
    protected PlayerController player;

    public State(PlayerController player)
    {
        this.player = player;
    }

    public abstract void Enter();
    public abstract void Update();
    public abstract void Exit();
}

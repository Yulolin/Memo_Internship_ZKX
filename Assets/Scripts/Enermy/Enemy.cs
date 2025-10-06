using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    public virtual void OnAttacked()
    {
        // 实现敌人被攻击的逻辑
        Debug.Log("Enemy has been attacked!");
    }
}

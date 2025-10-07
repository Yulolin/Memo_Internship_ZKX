using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spike : MonoBehaviour
{
    private void OnCollisionStay2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            // 获取碰撞接触点
            ContactPoint2D contact = other.contacts[0];
            if (contact.normal.y < 0)  // 下面碰撞
            {
                PlayerController player = other.gameObject.GetComponent<PlayerController>();
                player.ChangeState(player.hitState);
            }
        }
    }
}

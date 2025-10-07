using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrokenGround : MonoBehaviour
{
    public float BrokenTime = 2f;
    public float NullTime = 3f;
    public float RestoreTime = 3f;
    private Animator animator;
    private Collider2D collider2D;
    
    bool hasCollided = false;

    private void Start()
    {
        animator = GetComponent<Animator>();
        collider2D =  GetComponent<Collider2D>();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            // 获取碰撞接触点
            ContactPoint2D contact = other.contacts[0];
            if (!hasCollided&&contact.normal.y < 0)  // 下面碰撞
            {
                hasCollided = true;
                StartCoroutine(StartBroke());
            }
        }
    }

    IEnumerator StartBroke()
    {
        yield return new WaitForSeconds(BrokenTime);
        collider2D.enabled = false;
        animator.SetTrigger("Broken");
        yield return new WaitForSeconds(RestoreTime);
        GetComponent<SpriteRenderer>().enabled = false;
        yield return new WaitForSeconds(NullTime);
        animator.SetTrigger("Restore");
        GetComponent<SpriteRenderer>().enabled = true;
        yield return new WaitForSeconds(RestoreTime);
        collider2D.enabled = true;
        hasCollided = false;
    }
}

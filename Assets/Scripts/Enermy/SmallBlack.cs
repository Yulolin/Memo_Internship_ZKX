using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class SmallBlack : Enemy
{
    public float moveSpeed = 5;
    public Vector2 jumpVelovity = new Vector2(5,20);
    [Range(0, 100)] public int jumpRatio = 10;

    public bool isRight = false;
    private Rigidbody2D rb;
    Coroutine blackCoroutine;
    protected void Start()
    {
        base.Start();
        rb = gameObject.GetComponent<Rigidbody2D>();
        blackCoroutine = StartCoroutine(BlackMove());
    }
    private bool canChangeDir = true;
    private bool canJump = false;

    private void OnCollisionStay2D(Collision2D other)
    {
        // Debug.Log(other.gameObject.name);
        if (other.gameObject.CompareTag("Ground"))
        {
            // 获取碰撞接触点
            // ContactPoint2D contact = other.contacts[0];
            // if(contact.normal.y>0)
            canJump = true;
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground")||other.gameObject.CompareTag("Wall"))
        {
            // 获取碰撞接触点
            ContactPoint2D contact = other.contacts[0];
            Debug.Log(contact.normal);
            if (contact.normal.y < 0)
            {
                canJump = true;
            }
            if (contact.normal.x < 0)
            {
                isRight = false;
                transform.localScale = new Vector3(isRight?-1:1, 
                    transform.localScale.y, transform.localScale.z);
            }
            else if (contact.normal.x > 0)
            {
                isRight = true;
                transform.localScale = new Vector3(isRight?-1:1, 
                    transform.localScale.y, transform.localScale.z);
            }
        }
        // else if (other.gameObject.CompareTag("Wall"))
        // {
        //     if (canChangeDir)
        //     {
        //         canChangeDir = false;
        //         isRight = !isRight;
        //         transform.localScale = new Vector3(isRight?-1:1, 
        //             transform.localScale.y, transform.localScale.z);
        //     }
        // }
        else if (other.gameObject.CompareTag("Player"))
        {
            PlayerController player = other.gameObject.GetComponent<PlayerController>();
            other.gameObject.GetComponent<PlayerController>().ChangeState(player.hitState);
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {

        if (other.gameObject.CompareTag("Ground"))
        {
            // 获取碰撞接触点
            // ContactPoint2D contact = other.contacts[0];
            // if(contact.normal.y>0)
                canJump = false;
        }
        // else if (other.gameObject.CompareTag("Wall"))
        // {
        //     if (canChangeDir)
        //     {
        //         canChangeDir = true;
        //     }
        // }
    }
    IEnumerator BlackMove()
    {
        while (true)
        {
            rb.position += isRight?new Vector2(moveSpeed*Time.deltaTime,0) : new Vector2(-moveSpeed*Time.deltaTime,0);
            int rand = Random.Range(0, 100);
            if (rand < jumpRatio && canJump)
            {
                Vector2 force = jumpVelovity;
                force.x *= isRight ? 1 : -1;
                rb.velocity = force;
            }
            yield return null;
        }
    }

    public override void OnAttacked()
    {
        base.OnAttacked();
        StopCoroutine(blackCoroutine);
        audioSource.clip = AudioManager.Instance.EnemyBeAttacked;
        audioSource.Play();
        animator.SetBool("Dead",true);
        rb.bodyType = RigidbodyType2D.Dynamic;
        rb.velocity = new Vector2(0,10);
        Collider2D col =  gameObject.GetComponent<Collider2D>();
        col.enabled = false;
        Dead();
    }
}

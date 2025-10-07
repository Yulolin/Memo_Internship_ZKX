using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BlueBird : Enemy
{
    public float flySpeed = 5;
    public bool isRight = false;

    private Rigidbody2D rb;
    Coroutine flyCoroutine;
    protected void Start()
    {
        base.Start();
        if (isRight)
        {
            animator.SetBool("RightFly",true);
        }
        else{
            animator.SetBool("LeftFly",true);
        }
        rb= GetComponent<Rigidbody2D>();
        
        flyCoroutine = StartCoroutine(Fly());
    }

    IEnumerator Fly()
    {
        while (true)
        {
            if (isRight)
            {
                rb.position += new Vector2(flySpeed * Time.deltaTime, 0);
                animator.SetBool("RightFly",true);
                animator.SetBool("LeftFly",false);
            }
            else
            {
                rb.position -= new Vector2(flySpeed * Time.deltaTime, 0);
                animator.SetBool("RightFly",false);
                animator.SetBool("LeftFly",true);
            }
            yield return null;
        }
    }

    private bool canChangeDir = true;
    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.gameObject.CompareTag("Ground") || other.gameObject.CompareTag("Wall"))
        {
            if (canChangeDir)
            {
                canChangeDir = false;
                isRight = !isRight;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Ground") || other.gameObject.CompareTag("Wall"))
        {
            if (!canChangeDir)
            {
                canChangeDir =  true;
            }
        }
    }

    public override void OnAttacked()
    {
        base.OnAttacked();
        StopCoroutine(flyCoroutine);
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

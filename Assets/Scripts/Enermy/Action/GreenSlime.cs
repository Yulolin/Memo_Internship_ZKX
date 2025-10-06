using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class GreenSlime : Enemy
{
    [SerializeField] private Transform left;
    [SerializeField] private Transform right;
    private Animator  animator;
    public float jumpTime = 0.833f;
    public bool isRight = true;

    public float jumpDis = 5f;
    public float jumpHeight = 0.5f;
    // 二次函数的k项
    private Vector3 velocity;             // 当前的速度（水平和垂直分量）
    
    Coroutine jumpCoroutine;
    private void Start()
    {
        animator = GetComponent<Animator>();
        if (isRight)
        {
            animator.SetBool("RightJump",true);
        }
        else{
            animator.SetBool("LeftJump",true);
        }
        jumpCoroutine = StartCoroutine(StartAJump());
    }
    
    float jumpTimeCounter = 0f;
    IEnumerator StartAJump()
    {
        Vector3 startPos = transform.position;
        Vector3 nextPos = startPos;
        if (isRight)
        {
            nextPos.x += jumpDis;
        }
        else
        {
            nextPos.x -= jumpDis;
            
        }
        Vector3 highPos = (startPos + nextPos) / 2 + new Vector3(0, jumpHeight, 0);

        while (jumpTimeCounter < jumpTime)
        {
            float step = jumpTimeCounter / jumpTime;
            if (step > 0.99)
            {
                step = 1;
                jumpTimeCounter = jumpTime;
            }
            // Debug.Log(step);
            Vector3 currentPos = (1 - step) * (1 - step) * startPos + 2 * (1 - step) * step * highPos +
                                 step * step * nextPos;
            transform.position = currentPos;
            jumpTimeCounter+=Time.deltaTime;
            yield return null;
        }
        transform.position = new Vector3(transform.position.x, startPos.y, transform.position.z);
        if (isRight&&transform.position.x >= right.position.x)
        {
            isRight = false;
            animator.SetBool("RightJump",false);
            animator.SetBool("LeftJump",true);
        }
        else if (!isRight && transform.position.x <= left.position.x)
        {
            isRight = true;
            animator.SetBool("LeftJump",false);
            animator.SetBool("RightJump",true);
        }

        jumpTimeCounter = 0;
        jumpCoroutine = StartCoroutine(StartAJump());
    }

    public override void OnAttacked()
    {
        animator.SetBool("Dead",true);
        StopCoroutine(jumpCoroutine);
        Rigidbody2D rb = gameObject.GetComponent<Rigidbody2D>();
        rb.bodyType = RigidbodyType2D.Dynamic;
        rb.velocity = new Vector2(0,10);
        Collider2D col = gameObject.GetComponent<Collider2D>();
        col.enabled = false;
        Dead();
    }


}

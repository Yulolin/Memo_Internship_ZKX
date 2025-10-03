using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenSlime : MonoBehaviour
{
    [SerializeField] private Transform left;
    [SerializeField] private Transform right;
    private Animator  animator;
    public float deadTime = 0.333f;
    public float jumpTime = 0.833f;
    public bool isRight = true;

    public float jumpDis = 5f;
    public float jumpHeight = 0.5f;
    // 二次函数的k项
    private float k = 0;
    private Vector3 velocity;             // 当前的速度（水平和垂直分量）
    private void Start()
    {
        animator = GetComponent<Animator>();
        k = -jumpHeight * 4 / jumpDis / jumpDis;
        if (isRight)
        {
            animator.SetBool("RightJump",true);
        }
        else{
            animator.SetBool("LeftJump",true);
        }
        StartCoroutine(StartAJump());
    }
    
    float jumpTimeCounter = 0f;
    IEnumerator StartAJump()
    {

        Vector3 pos = transform.position;
        while (jumpTimeCounter < jumpTime)
        {
            // 获取动画的播放进度（normalizedTime）
            float normalizedTime = animator.GetCurrentAnimatorStateInfo(0).normalizedTime % 1f;
            if (isRight)
            {
                float x = normalizedTime * jumpDis;
                float y = k * (x - 0.5f * jumpDis) * (x - 0.5f * jumpDis) + jumpHeight;
                transform.position =
                    new Vector3(pos.x + x, pos.y + y, pos.z);
                jumpTimeCounter += Time.deltaTime;
            }
            else
            {
                float x = -normalizedTime * jumpDis;
                float y = k * (x + 0.5f * jumpDis) * (x + 0.5f * jumpDis) + jumpHeight;
                transform.position =
                    new Vector3(pos.x + x, pos.y + y, pos.z);
                jumpTimeCounter += Time.deltaTime;
            }
            yield return null;
        }

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
        StartCoroutine(StartAJump());
    }
    
}

using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;      // 移动速度
    public float jumpForce = 7f;      // 跳跃力量

    public float arrowDelay = 0.2f;
    public float attackDuration = 0.5f; // 攻击延迟

    public float arrowSpeed = 10f;
    // 箭矢预制体和生成位置
    public Transform arrowPos;
    
    private bool _isGrounded = false;
    public bool isGrounded { get{return _isGrounded;} }
    private bool isAttacking = false;

    public float invincibleTime = 3f;
    public bool isInvincible = false;

    private Rigidbody2D rb;
    public Animator animator;
    public AudioSource audioSource;
    
    private State currentState;
    public IdleState idleState;
    private MoveState moveState;
    private JumpState jumpState;
    private AttackState attackState;
    public HitState hitState;
    void Start()
    {
        rb =  GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        idleState = new IdleState(this);
        moveState = new MoveState(this);
        jumpState = new JumpState(this);
        attackState = new AttackState(this);
        hitState = new HitState(this);
        
        currentState = idleState;
        currentState.Enter();
    }

    // Update is called once per frame
    float horizontalInput;
    float verticalInput;
    void Update()
    {
        currentState.Update();
    }

    public void Move(bool canChangeState)
    {
        horizontalInput = Input.GetAxis("Horizontal");
        if (horizontalInput > 1e-6)
        {
            transform.localScale = new Vector3(MathF.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            if (canChangeState)
                ChangeState(moveState);
        }
        else if (horizontalInput < - 1e-6)
        {
            transform.localScale = new Vector3(-MathF.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            if (canChangeState)
                ChangeState(moveState);
        }
        rb.velocity = new Vector2(horizontalInput * moveSpeed, rb.velocity.y);
    }

    public void Jump()
    {
        if (Input.GetKeyDown(KeyCode.W) && isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            ChangeState(jumpState);
            audioSource.clip = AudioManager.Instance.PlayerJump;
            audioSource.Play();
        }
        else if (!isGrounded && rb.velocity.y < -1e-6)
        {
            ChangeState(jumpState);
        }
    }

    public void Attack()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ChangeState(attackState);
        }
    }
    // 生成箭矢
    public void CreateArrow()
    {
        GameObject arrow =  ArrowPool.Instance.GetArrow(arrowPos);
        
        Rigidbody2D arrowRB = arrow.GetComponent<Rigidbody2D>();
        // 调整朝向
        bool facingRight = transform.localScale.x > 0;
        Vector3 arrowScale = arrowRB.transform.localScale;
        arrowScale.x *= facingRight ? arrowScale.x : -arrowScale.x;
        arrowRB.transform.localScale = arrowScale;
        
        arrowRB.velocity = (facingRight ? Vector2.right : Vector2.left) * arrowSpeed;
    }
    // 改变状态
    public void ChangeState(State newState)
    {
        if (newState == currentState)
        {
            return;
        }

        if (isInvincible&&newState==hitState)
        {
            return;
        }
        currentState.Exit();
        currentState = newState;
        currentState.Enter();
    }
    // 开始一个协程，供状态类调用
    public Coroutine StartACoroutine(IEnumerator enumerator)
    {
        return StartCoroutine(enumerator);
    }
    // 停止一个协程，供状态类调用
    public void StopACoroutine(IEnumerator enumerator)
    {
        StopCoroutine(enumerator);
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground")&&MathF.Abs(rb.velocity.y)<1e-5)
        {
            _isGrounded = true;
            ChangeState(idleState);
        }
    }

    private void OnCollisionStay2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground")&&MathF.Abs(rb.velocity.y)<1e-5)
        {
            _isGrounded = true;
            // Debug.Log(isGrounded);
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground")&&MathF.Abs(rb.velocity.y)>1e-5)
        {
            _isGrounded = false;
            ChangeState(jumpState);
        }
    }
}

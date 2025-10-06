using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowController : MonoBehaviour
{
    private Rigidbody2D rb;

    private PlatformEffector2D platform;

    private Collider2D collider; 
    // 没插到墙上的时间
    public float ExistTime = 5f;
    // 插在墙上的时间
    public float ExistOnWallTime = 10f;
    // 最后闪烁的时间
    public float BlinkTime = 3f;
    // 协程
    Coroutine currentCoroutine;
    Coroutine blinkCoroutine;
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        platform =  GetComponent<PlatformEffector2D>();
        collider = GetComponent<Collider2D>();
        // rb.velocity = transform.right * speed;
    }

    private void OnEnable()
    {
        currentCoroutine = StartCoroutine(ExistWithoutWall());
        platform.enabled = false;
        collider.usedByEffector = false;
        rb.bodyType = RigidbodyType2D.Dynamic;
        gameObject.tag = "Prop";
    }

    public void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            other.gameObject.GetComponent<Enemy>().OnAttacked();
            Debug.Log("射到敌人");
            ArrowPool.Instance.ReturnArrow(gameObject);
        }
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            other.GetComponent<Enemy>().OnAttacked();
            Debug.Log("射到敌人");
            ArrowPool.Instance.ReturnArrow(gameObject);
        }
    }

    public void TouchWall()
    {
        StopCoroutine(currentCoroutine);
        StopCoroutine(blinkCoroutine);
        GetComponent<SpriteRenderer>().color = Color.white;
        currentCoroutine = StartCoroutine(ExistOnWall());
        gameObject.tag = "Ground";
        platform.enabled = true;
        rb.velocity = Vector2.zero;
        rb.bodyType = RigidbodyType2D.Kinematic;
        collider.usedByEffector = true;
        Debug.Log("射到墙");
    }

    IEnumerator ExistWithoutWall()
    {
        // 播放闪烁特效
        blinkCoroutine = StartCoroutine(SpriteBlinkUtil.AcceleratedBlinking(GetComponent<SpriteRenderer>(), BlinkTime));
        yield return new WaitForSeconds(ExistTime);
        ArrowPool.Instance.ReturnArrow(gameObject);
    }

    IEnumerator ExistOnWall()
    {
        yield return new WaitForSeconds(ExistOnWallTime - BlinkTime);
        // 播放闪烁特效
        blinkCoroutine = StartCoroutine(SpriteBlinkUtil.AcceleratedBlinking(GetComponent<SpriteRenderer>(), BlinkTime));
        yield return new WaitForSeconds(BlinkTime);
        platform.enabled = false;
        collider.usedByEffector = false;
        rb.bodyType = RigidbodyType2D.Dynamic;
        gameObject.tag = "Prop";
        ArrowPool.Instance.ReturnArrow(gameObject);
    }
}

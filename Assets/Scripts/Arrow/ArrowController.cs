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
    // 协程
    Coroutine currentCoroutine;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        platform =  GetComponent<PlatformEffector2D>();
        collider = GetComponent<Collider2D>();
        // rb.velocity = transform.right * speed;
        currentCoroutine = StartCoroutine(ExistWithoutWall());
    }

    public void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            Destroy(gameObject);
            Debug.Log("射到敌人");
        }
    }

    public void TouchWall()
    {
        StopCoroutine(currentCoroutine);
        currentCoroutine = StartCoroutine(ExistOnWall());
        platform.enabled = true;
        rb.velocity = Vector2.zero;
        rb.isKinematic = true;
        collider.usedByEffector = true;
        Debug.Log("射到墙");
    }

    IEnumerator ExistWithoutWall()
    {
        yield return new WaitForSeconds(ExistTime);
        gameObject.SetActive(false);
    }

    IEnumerator ExistOnWall()
    {
        yield return new WaitForSeconds(ExistOnWallTime);
        gameObject.SetActive(false);
        platform.enabled = false;
        collider.usedByEffector = false;
    }
}

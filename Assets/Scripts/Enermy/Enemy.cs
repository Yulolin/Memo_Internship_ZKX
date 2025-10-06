using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    public AudioSource audioSource;
    protected void Start()
    {
        LevelManager.Instance.RegisterEnemy(this);
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;
    }
    public virtual void OnAttacked()
    {
        // 实现敌人被攻击的逻辑
        Debug.Log("Enemy has been attacked!");
    }
    public virtual void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            PlayerController player = other.gameObject.GetComponent<PlayerController>();
            other.GetComponent<PlayerController>().ChangeState(player.hitState);
        }
    }
    public virtual async Task Dead()
    {
        GameObject coin = CoinPool.Instance.GetRandomCoin();
        coin.transform.position = transform.position;
        Rigidbody2D rb = coin.GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(0, coin.GetComponent<Coin>().CreateVelocityY);
        await Task.Delay(3000);
        Destroy(gameObject.transform.parent.gameObject);
        LevelManager.Instance.RemoveEnemy(this);
    } 
}

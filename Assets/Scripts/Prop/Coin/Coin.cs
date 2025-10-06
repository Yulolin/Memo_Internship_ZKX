using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Coin : MonoBehaviour
{
    public float CreateVelocityX = 5f;
    public float CreateVelocityY = 20f;
    
    public int Score;

    public AudioSource audioSource;

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            audioSource.Play();
            PlayerManager.Instance.AddScore(Score);
            if (Score == 100)
            {
                CoinPool.Instance.ReturnGoldCoin(gameObject);
            }
            else if (Score == 50)
            {
                CoinPool.Instance.ReturnSilverCoin(gameObject);
            }
        }
    }
}


using System;
using UnityEngine;

public class AudioManager :MonoBehaviour
{
    public static AudioManager Instance;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public AudioClip EnemyBeAttacked;
    public AudioClip ArrowCreate;
    public AudioClip PlayerJump;
    public AudioClip PlayerHit;
}

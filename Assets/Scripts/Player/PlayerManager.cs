using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PlayerData
{
    public int level;
}

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager Instance = null;

    public int HP = 3;
    public int Score = 0;
    
    PlayerData playerData;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void ChangeHP(int value)
    {
        HP += value;
    }

    public void AddScore(int value)
    {
        Score += value;
    }
}

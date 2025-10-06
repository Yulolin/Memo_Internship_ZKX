using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;
    public int level = 0;
    public int enemyCount = 0;
    
    public Action<int> OnlevelSuccess;
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

    public void StartLevel(int level)
    {
        this.level = level;
        enemyCount = 0;
        SceneManager.LoadScene("Level" + level);
        PlayerManager.Instance.ResetPlayerStatus();
    }

    public void RegisterEnemy(Enemy enemy)
    {
        enemyCount++;
    }

    public void RemoveEnemy(Enemy enemy)
    {
        enemyCount--;
        if (enemyCount == 0)
        {
            OnlevelSuccess?.Invoke(level);
        }
    }

}

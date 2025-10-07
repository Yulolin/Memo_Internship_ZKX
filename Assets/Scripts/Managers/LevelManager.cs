using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = System.Random;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;
    public int level = 0;
    public int enemyCount = 0;
    public bool isInLevel = false;
    
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
        Time.timeScale = 1;
        isInLevel =  true;
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
            isInLevel =  false;
        }
    }

    public List<GameObject> Props = new List<GameObject>();
    public float PropCreateTime = 10f;
    private float timer = 0f;
    private void Update()
    {
        if (isInLevel)
        {
            timer += Time.deltaTime;
            if (timer > PropCreateTime)
            {
                Vector2 pos = new Vector2(0, 30);
                Instantiate(Props[UnityEngine.Random.Range(0, Props.Count)], pos, Quaternion.identity);
                timer = 0;
            }
        }
    }
}

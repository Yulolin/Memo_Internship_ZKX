using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PlayerData
{
    public int level = 1;
    public int totalScore = 0;
}

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager Instance = null;

    public int HP = 3;
    public int Score = 0;

    public Action<int> OnHpChange;
    public Action<int> OnScoreChange;
    public Action OnLevelFail;
    
    private PlayerData _playerData;

    public PlayerData PlayerData
    {
        get
        {
            if (_playerData == null)
            {
                // 从 PlayerPrefs 中获取存储的 JSON 字符串
                string json = PlayerPrefs.GetString("PlayerData", "{}"); // 默认值是一个空的 JSON 对象
                // 如果获取的 JSON 字符串不是空的，进行反序列化
                if (!json.Equals("{}"))
                {
                    // 将 JSON 字符串反序列化为 PlayerData 对象
                    _playerData =  JsonUtility.FromJson<PlayerData>(json);
                }
                else
                {
                    // 如果没有数据，返回一个默认的 PlayerData 对象
                    _playerData =  new PlayerData(); // 默认等级 1
                }
            }
            return  _playerData;
        }
    }
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

    private void Start()
    {
        LevelManager.Instance.OnlevelSuccess += UpdateLevel;
    }

    private void OnDestroy()
    {
        LevelManager.Instance.OnlevelSuccess -= UpdateLevel;

    }

    public void SavePlayerData()
    {
        // 将 PlayerData 对象序列化为 JSON 字符串
        string json = JsonUtility.ToJson(_playerData);
        // 存储到 PlayerPrefs 中
        PlayerPrefs.SetString("PlayerData", json);
        // 保存到磁盘
        PlayerPrefs.Save();
    }

    public void ResetPlayerStatus()
    {
        HP = 3;
        OnHpChange?.Invoke(HP);
        Score = 0;
        OnScoreChange?.Invoke(Score);
    }
    public void ChangeHP(int value)
    {
        HP += value;
        if (HP > 3)
            HP = 3;
        else if (HP == 0)
        {
            OnLevelFailed();
        }
        OnHpChange?.Invoke(HP);
    }

    public void AddScore(int value)
    {
        Score += value;
        OnScoreChange(Score);
    }

    public int GetTotalScore()
    {
        return PlayerData.totalScore;
    }

    public void UpdateLevel(int level)
    {
        if (level == PlayerData.level)
        {
            PlayerData.level++;
            SavePlayerData();
        }
    }

    public void OnLevelFailed()
    {
        PlayerData.totalScore+= Score;
        SavePlayerData();
        OnLevelFail?.Invoke();
    }
}

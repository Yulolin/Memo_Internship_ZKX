using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameUI : MonoBehaviour
{

    
    [Header("常规界面")]
    [SerializeField] private List<GameObject> player1HP = new List<GameObject>();
    [SerializeField] private Text scoreText;
    [SerializeField] private Button pauseBtn;
    [SerializeField] private Button musicBtn;
    [SerializeField] private Button soundFXBtn;
    
    [Header("暂停界面")]
    [SerializeField] private GameObject pausePanel;
    [SerializeField] private Button resumeBtn;
    [SerializeField] private Button endGameBtn;

    [Header("胜利界面")] 
    [SerializeField] private GameObject levelSuccessPanel;
    [SerializeField] private Text levelScoreTxt;
    [SerializeField] private Text totalScoreTxt;
    
    [SerializeField] private Button continueBtn;
    [SerializeField] private Button successEndGameBtn;
    
    [Header("失败界面")] 
    [SerializeField] private GameObject levelFailPabel;
    
    // 胜利逻辑
    void Start()
    {
        // 常规界面
        pauseBtn.onClick.AddListener(OnPauseBtnClick);
        musicBtn.onClick.AddListener(OnMusicBtnClick);
        soundFXBtn.onClick.AddListener(OnSoundFXBtnClick);
        // 暂停界面
        resumeBtn.onClick.AddListener(OnResumeBtnClick);
        endGameBtn.onClick.AddListener(OnEndGameBtnClick);
        // 胜利界面
        continueBtn.onClick.AddListener(OnContinueBtnClick);
        successEndGameBtn.onClick.AddListener(OnEndGameBtnClick);
        // 订阅hp，score通知
        PlayerManager.Instance.OnHpChange += UpdateHP;
        PlayerManager.Instance.OnScoreChange += UpdateScore;
        // 订阅胜利通知
        LevelManager.Instance.OnlevelSuccess += OnLevelSuccess;
    }

    private void OnDestroy()
    {
        // 订阅hp，score通知
        PlayerManager.Instance.OnHpChange -= UpdateHP;
        PlayerManager.Instance.OnScoreChange -= UpdateScore;
        LevelManager.Instance.OnlevelSuccess -= OnLevelSuccess;
    }

    #region 常规界面
    public void OnPauseBtnClick()
    {
        Time.timeScale = 0;
        pausePanel.SetActive(true);
    }

    public void OnMusicBtnClick()
    {
        
    }

    public void OnSoundFXBtnClick()
    {
        
    }
    
    public void UpdateHP(int hp)
    {
        for (int i = 0; i < 3; i++)
        {
            if (i < hp)
            {
                player1HP[i].SetActive(true);
            }
            else
            {
                player1HP[i].SetActive(false);
            }
        }
    }
    public void UpdateScore(int score)
    {
        scoreText.text = score.ToString();
    }
    #endregion

    #region 暂停界面
    public void OnResumeBtnClick()
    {
        Time.timeScale = 1;
        pausePanel.SetActive(false);
    }

    public void OnEndGameBtnClick()
    {
        SceneManager.LoadScene("StartScene");
    }

    #endregion

    #region 胜利界面

    public void OnLevelSuccess(int level)
    {
        levelSuccessPanel.SetActive(true);
        levelScoreTxt.text = "Level score : " + PlayerManager.Instance.Score;
        totalScoreTxt.text = "Total score : " + PlayerManager.Instance.GetTotalScore();
    }
    public void OnContinueBtnClick()
    {
        LevelManager.Instance.StartLevel(LevelManager.Instance.level + 1);
    }

    #endregion
}

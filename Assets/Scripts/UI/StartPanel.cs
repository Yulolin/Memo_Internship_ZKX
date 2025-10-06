using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartPanel : MonoBehaviour
{
    [SerializeField] private Button playBtn;

    private void Start()
    {
        playBtn.onClick.AddListener(OnPlayBtnClick);
    }

    private void OnDestroy()
    {
        playBtn.onClick.RemoveListener(OnPlayBtnClick);
    }

    public void OnPlayBtnClick()
    {
        LevelManager.Instance.StartLevel(1);
    }
}

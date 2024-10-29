using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Mime;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class MainGamePanel : MonoBehaviour
{
    private Text scoreText;
    private Button restartBtn;
    private Button returnStartBrn;
    void Start()
    {
        scoreText = transform.Find("scoreText").GetComponent<Text>();
        restartBtn = transform.Find("restartBtn").GetComponent<Button>();
        returnStartBrn = transform.Find("returnStartBrn").GetComponent<Button>();
        restartBtn.AddOnClick(() =>
        {
            GameMgr.Instance.RestartGame();
        });
        returnStartBrn.AddOnClick(() =>
        {
            GameMgr.Instance.ReturnGameStart();
        });
        EventHandler.UpdateScoreText += OnUpdateScoreTextEvent;
        SetScoreText();
    }

    private void SetScoreText()
    {
        scoreText.text = $"得分：{GameMgr.Instance.Score.ToString()}";
    }

    private void OnUpdateScoreTextEvent()
    {
        SetScoreText();
    }

    
    private void OnDestroy()
    {
        EventHandler.UpdateScoreText -= OnUpdateScoreTextEvent;
    }
}

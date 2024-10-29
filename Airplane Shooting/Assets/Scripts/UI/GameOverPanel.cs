using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverPanel : MonoBehaviour
{
    private Button restartBtn;
    private Button returnStartBrn;
    void Start()
    {
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
    }
}

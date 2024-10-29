using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class StartGamePanel : MonoBehaviour
{
    private Button startBtn;
    private void Start()
    {
        startBtn = transform.Find("startGameBtn").GetComponent<Button>();
        startBtn.AddOnClick(GameMgr.Instance.StartGame);
    }
}

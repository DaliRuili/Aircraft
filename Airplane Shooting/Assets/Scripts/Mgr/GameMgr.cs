using UnityEngine;

public class GameMgr
{
    private static GameMgr instance;

    public static GameMgr Instance
    {
        get
        {
            if (instance != null)
            {
                return instance;
            }

            instance = new GameMgr();
            return instance;
        }
    }

    private GameState gameState;
    public PlayerAircraft player;
    private int m_score;

    public int Score
    {
        get => m_score;
        set
        {
            m_score = value;
            EventHandler.CallUpdateScoreText();
        }
    }

    public void Run()
    {
        gameState = GameState.Ready;
        UIMgr.Instance.ShowUI(Const.StartGamePanel);
    }
    public void StartGame()
    {
        gameState = GameState.Playing;
        UIMgr.Instance.HideUI(Const.StartGamePanel);
        UIMgr.Instance.ShowUI(Const.MainGamePanel);
        //创建主角飞机
        player = (PlayerAircraft)AircraftFactory.CreateAircraft(AircraftType.Player);
        Score = 0;
    }
    public void OnUpdate()
    {
        if (gameState == GameState.Playing)
        {
            AircraftFactory.Update();
        }
    }

    /// <summary>
    /// 游戏结束
    /// </summary>
    public void GameOver()
    {
        gameState = GameState.End;
        ClearObjs();
        UIMgr.Instance.ShowUI(Const.GameOverPanel);
    }

    private void ClearObjs()
    {
        if (player != null)
        {
            player.DestroySelf();
        }
        UIMgr.Instance.HideUI(Const.GameOverPanel);
        AircraftFactory.ClearAll();
        EnemyBulletGenerator.CLear();
    }
    
    /// <summary>
    /// 从新开始游戏
    /// </summary>
    public void RestartGame()
    {
        gameState = GameState.End;
        ClearObjs();
        UIMgr.Instance.HideUI(Const.MainGamePanel);
        UIMgr.Instance.HideUI(Const.GameOverPanel);
        StartGame();
    }
    /// <summary>
    /// 返回主界面
    /// </summary>
    public void ReturnGameStart()
    {
        gameState = GameState.Ready;
        ClearObjs();
        UIMgr.Instance.HideUI(Const.MainGamePanel);
        UIMgr.Instance.HideUI(Const.GameOverPanel);
        Run();
    }
}

public enum GameState
{
    Ready,
    Playing,
    End
}
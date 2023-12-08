using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverMgr : MonoBehaviour
{
    public Text HighScoreText;
    public Text CurrentScoreText;
    public Button RestartBtn;
    public Button ClearDataBtn;

    void Start()
    {
        if (GameMgr.m_BestHeight < GameMgr.m_CurBHeight)
        {
            GameMgr.m_BestHeight = GameMgr.m_CurBHeight;
            GameMgr.Save();
        }
            if (HighScoreText != null)
                HighScoreText.text = "최고 기록 : " + GameMgr.m_BestHeight.ToString("F2");
            if (CurrentScoreText != null)
                CurrentScoreText.text = "이번 기록 : " + GameMgr.m_CurBHeight.ToString("F2");
        

        if (RestartBtn != null)
            RestartBtn.onClick.AddListener(() => SceneManager.LoadScene("GameScene"));
        if (ClearDataBtn != null)
            ClearDataBtn.onClick.AddListener(CD_BtnClick);

    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Return) == true)
        {
            SceneManager.LoadScene("GameScene");
        }

    }


    void CD_BtnClick()
    {
        PlayerPrefs.DeleteAll();
        GameMgr.m_CurBHeight = 0.0f;

        GameMgr.Load();

        if (HighScoreText != null)
            HighScoreText.text = "최고 기록 : " + GameMgr.m_BestHeight.ToString("N2");
        if (CurrentScoreText != null)
            CurrentScoreText.text = "이번 기록 : " + GameMgr.m_CurBHeight.ToString("N2");
    }

}

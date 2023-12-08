using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameMgr : MonoBehaviour
{
    GameObject player;

    public Text CurHeight_Text;
    public Text BestHeight_Text;

    float m_Height = 0.0f; // 현재 높이
    public static float m_CurBHeight = 0.0f; //이번판 최고 높이    //GameOver Scene에서 출력하기 위함
    public static float m_BestHeight = 0.0f; //역대 최고 높이      //GameOver Scene에서 출력하기 위함

    void Start()
    {
        Load();

        player = GameObject.Find("cat");
        m_CurBHeight = 0.0f;
    }

    void Update()
    {
        // 높이값 기록
        m_Height = player.transform.position.y;
        if (m_Height < 0.0f)
            m_Height = 0.0f;
        if (m_CurBHeight < m_Height)
            m_CurBHeight = m_Height;

        if(m_BestHeight < m_CurBHeight)
        {
            m_BestHeight = m_CurBHeight;
            Save();
        }

        if (CurHeight_Text != null)
            CurHeight_Text.text = "높이 : " + m_CurBHeight.ToString("F2");
        if (BestHeight_Text != null)
            BestHeight_Text.text = "최고기록 : " + m_BestHeight.ToString("N2");

        
    }

    public static void Save()
    {
        PlayerPrefs.SetFloat("HighScore", m_BestHeight);
    }

    public static void Load()
    {
        m_BestHeight = PlayerPrefs.GetFloat("HighScore", 0.0f);
    }

}

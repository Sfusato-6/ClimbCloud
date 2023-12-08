using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudWvGenerator : MonoBehaviour
{
    public GameObject cloudWave;
    float recentHeight = -2.5f; //마지막으로 생성된 구름층의 높이

    GameObject player;
    float createHeight = 10.0f; //주인공으로부터 머리위로 10.0m 위까지만 구름층을 생성

    void Start()
    {
        this.player = GameObject.Find("cat");
        //for (int i = 0; i < 50; i++) 
        //{
        //    SpawnCloudWave(recentHeight);
        //    recentHeight += 2.5f;
        //}

    }

    void Update()
    {
        Vector3 playerPos = this.player.transform.position;

        //일정 높이에 구름층 생성 -> 플레이어 위치 기준 10.0f 위에 계속 생성
        if(recentHeight < playerPos.y + createHeight)
        {
            SpawnCloudWave(recentHeight);
            recentHeight += 2.5f;

        }

    }

    void SpawnCloudWave(float a_Height)
    {
        int a_Level = (int)(a_Height / 15.0f);

        int a_HideCount = 0;
        if (a_Level <= 0)
            a_HideCount = 0;
        else if (a_Level == 1)
            a_HideCount = Random.Range(0, 2); //0 ~ 1
        else if (a_Level == 2)
            a_HideCount = Random.Range(0, 3); // 0 ~ 2
        else if (a_Level == 3)
            a_HideCount = Random.Range(0, 4); // 1 ~ 2
        else if (a_Level == 4)
            a_HideCount = Random.Range(0, 5); // 1 ~ 3
        else 
            a_HideCount = Random.Range(2, 4); // 2 ~ 3

        GameObject Go = Instantiate(cloudWave) as GameObject;
        Go.transform.position = new Vector3(0.0f, a_Height, 0.0f);
        Go.GetComponent<CloudWaveCtrl>().SetHideCloud(a_HideCount);
    }

}

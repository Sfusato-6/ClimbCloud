using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudWvGenerator : MonoBehaviour
{
    public GameObject cloudWave;
    float recentHeight = -2.5f; //���������� ������ �������� ����

    GameObject player;
    float createHeight = 10.0f; //���ΰ����κ��� �Ӹ����� 10.0m �������� �������� ����

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

        //���� ���̿� ������ ���� -> �÷��̾� ��ġ ���� 10.0f ���� ��� ����
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

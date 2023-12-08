using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterCtrl : MonoBehaviour
{
    float speed = 1.0f; //1초에 1m를 움직이게 함

    GameObject player;
    float distanceltv = 8.0f; //주인공과의 거리가 8m 이상 벌어지지 않도록 함

    void Start()
    {
        player = GameObject.Find("cat");
    }

    void Update()
    {
        //Player와 거리가 너무 먼 경우 보정
        float a_FollowHeight = player.transform.position.y - distanceltv;
        if (transform.position.y < a_FollowHeight)
            transform.position = new Vector3(0.0f, a_FollowHeight, 0.0f);

        // 일정 속도로 위로 움직이게 함 -> 단일 사용 시 올라오는 속도가 느림
        transform.Translate(new Vector3(0.0f, speed * Time.deltaTime, 0.0f));
    }
}

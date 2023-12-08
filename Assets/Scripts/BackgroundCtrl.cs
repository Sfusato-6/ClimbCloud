using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundCtrl : MonoBehaviour
{
    GameObject player;
    float startY = 12.0f;   // 백그라운드의 시작 y 높이 위치
    float scroll = 0.2f;    // 백그라운드가 위로 올라가는 속도

    void Start()
    {
        player = GameObject.Find("cat");
        

    }

    void Update()
    {
        //오프셋값 조정으로 배경의 색상 서서히 변경
        float scrollPos = startY - player.transform.position.y * scroll;
        if (scrollPos > 12.0f)
            scrollPos = 12.0f;
        else if (scrollPos < -12.0f)
            scrollPos = -12.0f;

        transform.position = new Vector3(0.0f, player.transform.position.y + scrollPos, 0.0f);

    }
}

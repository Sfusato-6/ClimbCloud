using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterCtrl : MonoBehaviour
{
    float speed = 1.0f; //1�ʿ� 1m�� �����̰� ��

    GameObject player;
    float distanceltv = 8.0f; //���ΰ����� �Ÿ��� 8m �̻� �������� �ʵ��� ��

    void Start()
    {
        player = GameObject.Find("cat");
    }

    void Update()
    {
        //Player�� �Ÿ��� �ʹ� �� ��� ����
        float a_FollowHeight = player.transform.position.y - distanceltv;
        if (transform.position.y < a_FollowHeight)
            transform.position = new Vector3(0.0f, a_FollowHeight, 0.0f);

        // ���� �ӵ��� ���� �����̰� �� -> ���� ��� �� �ö���� �ӵ��� ����
        transform.Translate(new Vector3(0.0f, speed * Time.deltaTime, 0.0f));
    }
}

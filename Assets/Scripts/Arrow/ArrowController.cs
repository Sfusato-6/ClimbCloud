using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using UnityEngine;
using UnityEngine.UI;

public class ArrowController : MonoBehaviour
{
    float speed = 5.0f;
    GameObject player;

    public Image warningImg;
    float waitTime = 1.0f;

    void Start()
    {
        player = GameObject.Find("cat");
    }

    void Update()
    {
        //--- Warning 연출 관련 Update
        if(0.0f < waitTime)
        {
            waitTime -= Time.deltaTime;

            //깜빡임 연출
            WarningDirector();
            return;
        }
        if(warningImg.gameObject.activeSelf == true)
            warningImg.gameObject.SetActive(false);
        //--- Warning 연출 관련 Update


        transform.Translate(0.0f, -speed * Time.deltaTime, 0.0f);
        if (transform.position.y < player.transform.position.y - 10.0f)
            Destroy(gameObject);
    }

    /// <summary>
    /// 화살의 떨어질 위치를 설정하는 함수
    /// </summary>
    /// <param name="a_PosX" >랜덤한 X값을 매개변수로 Random.Range()로 받아서 활용</param>
    public void InitArrow(float a_PosX)
    {
        player = GameObject.Find("cat");
        transform.position = new Vector3(a_PosX * 1.1f, player.transform.position.y + 10.0f, 0.0f);
        // * 1.1f는 화살이 떨어지는 위치를 구름의 중앙에 맞춰주기 위해서 연산

        //경고 표시 위치 잡아줌
        Vector3 screenPos = Camera.main.WorldToScreenPoint(transform.position);
        warningImg.transform.position = new Vector3(screenPos.x, warningImg.transform.position.y, warningImg.transform.position.z);

    }

    float alpha = -6.0f; //투명도 변화 속도
    void WarningDirector() //깜빡임 투명도 변화 연출 함수
    {
        if (warningImg == null)
            return;

        if (warningImg.color.a >= 1.0f)
            alpha = -6.0f;
        else if (warningImg.color.a <= 0.0f)
            alpha = 6.0f;

        warningImg.color = new Color(1.0f, 1.0f, 1.0f, warningImg.color.a + alpha * Time.deltaTime);
    }
    
}

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
        //--- Warning ���� ���� Update
        if(0.0f < waitTime)
        {
            waitTime -= Time.deltaTime;

            //������ ����
            WarningDirector();
            return;
        }
        if(warningImg.gameObject.activeSelf == true)
            warningImg.gameObject.SetActive(false);
        //--- Warning ���� ���� Update


        transform.Translate(0.0f, -speed * Time.deltaTime, 0.0f);
        if (transform.position.y < player.transform.position.y - 10.0f)
            Destroy(gameObject);
    }

    /// <summary>
    /// ȭ���� ������ ��ġ�� �����ϴ� �Լ�
    /// </summary>
    /// <param name="a_PosX" >������ X���� �Ű������� Random.Range()�� �޾Ƽ� Ȱ��</param>
    public void InitArrow(float a_PosX)
    {
        player = GameObject.Find("cat");
        transform.position = new Vector3(a_PosX * 1.1f, player.transform.position.y + 10.0f, 0.0f);
        // * 1.1f�� ȭ���� �������� ��ġ�� ������ �߾ӿ� �����ֱ� ���ؼ� ����

        //��� ǥ�� ��ġ �����
        Vector3 screenPos = Camera.main.WorldToScreenPoint(transform.position);
        warningImg.transform.position = new Vector3(screenPos.x, warningImg.transform.position.y, warningImg.transform.position.z);

    }

    float alpha = -6.0f; //���� ��ȭ �ӵ�
    void WarningDirector() //������ ���� ��ȭ ���� �Լ�
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

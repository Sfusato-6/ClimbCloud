using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudWaveCtrl : MonoBehaviour
{
    GameObject player;
    float destroyDistance = 10.0f;  //���ΰ� �Ʒ������� 10m

    public GameObject[] Clouds;
    public GameObject Fish;

    void Start()
    {
        player = GameObject.Find("cat");
    }

    void Update()
    {
        Vector3 playerpos = player.transform.position;

        //���� �Ÿ� �Ʒ� �ı�
        if (transform.position.y < playerpos.y - destroyDistance)
            Destroy(gameObject);
    }


    public void SetHideCloud(int a_Count)
    {
        List<int> active = new List<int>();
        for (int i = 0; i < Clouds.Length; i++)
        {
            active.Add(i);
        }

        for (int i = 0; i < a_Count; i++) 
        {
            int ran = Random.Range(0, active.Count);
            Clouds[active[ran]].SetActive(false);

            active.RemoveAt(ran);
        }
        active.Clear();

        //����� ����
        int range = 10; // 10���� 1 Ȯ���� ���� ���� ������ ����
        SpriteRenderer[] a_CloudObj = GetComponentsInChildren<SpriteRenderer>();
        //Active�� Ȱ��ȭ �Ǿ� �ִ� ���� ��ϸ� �������� ���
        //�Ű����� includeInactive�� ��Ȱ��ȭ�� �ڽ� ������Ʈ�� ��ȯ�Ұ��� ���θ� �������ִ� ����
        //����Ʈ�� false�� ���� ��Ȱ��ȭ�� �ڽ� ������Ʈ���� ��ȯ�Ϸ��� ture�� ��������� ��
        for (int i = 0; i < a_CloudObj.Length; i++) 
        {
            if (Random.Range(0, range) == 0)
                SpawnFish(a_CloudObj[i].transform.position);
        }



    }

    void SpawnFish(Vector3 a_Pos)
    {
        GameObject go = Instantiate(Fish);
        go.transform.position = a_Pos + Vector3.up * 0.8f;
    }
}

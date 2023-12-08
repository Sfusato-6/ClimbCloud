using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudWaveCtrl : MonoBehaviour
{
    GameObject player;
    float destroyDistance = 10.0f;  //주인공 아래쪽으로 10m

    public GameObject[] Clouds;
    public GameObject Fish;

    void Start()
    {
        player = GameObject.Find("cat");
    }

    void Update()
    {
        Vector3 playerpos = player.transform.position;

        //일정 거리 아래 파괴
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

        //물고기 스폰
        int range = 10; // 10분의 1 확률로 구름 위에 아이템 생성
        SpriteRenderer[] a_CloudObj = GetComponentsInChildren<SpriteRenderer>();
        //Active가 활성화 되어 있는 구름 목록만 가져오는 방법
        //매개변수 includeInactive는 비활성화인 자식 오브젝트도 반환할건지 여부를 설정해주는 것임
        //디폴트는 false로 만약 비활성화인 자식 오브젝트까지 반환하려면 ture를 설정해줘야 함
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

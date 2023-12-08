using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement; //LoadScene을 사용하는 데 필요하다.
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D rigid2D;
    Animator animator;
    float jumpForce = 680.0f;
    float walkForce = 30.0f;
    float maxWalkSpeed = 2.0f;
    float walkSpeed = 3.0f;

    int m_ReserveJump = 0;  //점프 예약

    float hp = 3.0f;
    public Image[] hpImage;
    GameObject m_OverlapBlock = null;

    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 60;
        QualitySettings.vSyncCount = 0;

        this.rigid2D = GetComponent<Rigidbody2D>();
        this.animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //점프 예약
        if (Input.GetKeyDown(KeyCode.Space) == true)
        {
            m_ReserveJump = 3; //3 프레임
        }

        // 점프한다. -> float 오차값 해결해야 함
        //if(Input.GetKeyDown(KeyCode.Space) && this.rigid2D.velocity.y == 0) 
        if((-0.01f <= this.rigid2D.velocity.y && this.rigid2D.velocity.y <= 0.01f))
        {
            //3프레임 더 확인함
            if (0 < m_ReserveJump)
            {
                this.animator.SetTrigger("JumpTrigger");
                this.rigid2D.velocity = new Vector2(rigid2D.velocity.x, 0.0f);
                this.rigid2D.AddForce(transform.up * this.jumpForce);

                m_ReserveJump = 0;
            }
        }
        //Update함수 호출마다 감소함
        if (0 < m_ReserveJump)
            m_ReserveJump--;

        // 좌우 이동
        int key = 0;
        if (Input.GetKey(KeyCode.RightArrow)) key = 1;
        if (Input.GetKey(KeyCode.LeftArrow)) key = -1;

        // 플레이어의 속도
        float speedx = Mathf.Abs(this.rigid2D.velocity.x);

        //// 스피드 제한
        //if(speedx < this.maxWalkSpeed)
        //{
        //    this.rigid2D.AddForce(transform.right * key * this.walkForce);
        //}

        //캐릭터 이동
        rigid2D.velocity = new Vector2((key * walkSpeed), rigid2D.velocity.y);

        // 움직이는 방향에 따라 번전한다.
        if(key != 0)
        {
            transform.localScale = new Vector3(key, 1, 1);
        }

        // 플레이어 속도에 맞춰 애니메이션 속도를 바꾼다.
        if (this.rigid2D.velocity.y == 0)
        {
            this.animator.speed = speedx / 2.0f;
        }
        else
        {
            this.animator.speed = 1.0f;
        }

        // 플레이어가 화면 밖으로 나갔다면 처음부터
        if(transform.position.y < -10)
        {
            SceneManager.LoadScene("GameScene");
        }

        Vector3 pos = transform.position;
        if (pos.x < -2.5) pos.x = -2.5f;
        if (pos.x >  2.5) pos.x = 2.5f;
        transform.position = pos;

    }//void Update()

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.name.Contains("flag") == true)
        {
            Debug.Log("골");
            SceneManager.LoadScene("ClearScene");
        }
        else if (coll.gameObject.name.Contains("water") == true)
        {
            Die();
        }
        else if(coll.gameObject.name.Contains("arrow") == true)
        {
            //하트 하나 줄임
            if(m_OverlapBlock != coll.gameObject)
            {
                hp -= 1.0f;
                HpImageUpdate();
                if(hp <= 0.0f)
                {
                    Die();
                }
                m_OverlapBlock = coll.gameObject;
            }

            Destroy(coll.gameObject);
        }
        else if(coll.gameObject.name.Contains("fish") == true)
        {
            //hp 조금 회복
            if(m_OverlapBlock != coll.gameObject)
            {
                hp += 0.5f; //hp를 하트 반칸 회복함

                if (3.0f < hp)
                    hp = 3.0f;
                HpImageUpdate();

                m_OverlapBlock = coll.gameObject;
            }
            Destroy(coll.gameObject);
        }
    }


    void Die()
    {
        //게임 엔드 처리 필요
        SceneManager.LoadScene("GameOverScene");
    }

    void HpImageUpdate()
    {
        float a_CacHp = 0.0f;
        for(int i = 0; i < hpImage.Length; i++)
        {
            a_CacHp = hp - (float)i;
            if (a_CacHp < 0.0f)
                a_CacHp = 0.0f;
            if (1.0f < a_CacHp)
                a_CacHp = 1.0f;

            if (0.45f < a_CacHp && a_CacHp < 0.55f)
                a_CacHp = 0.445f;

            hpImage[i].fillAmount = a_CacHp;
        }
    }

}

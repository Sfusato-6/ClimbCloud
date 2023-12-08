using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement; //LoadScene�� ����ϴ� �� �ʿ��ϴ�.
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D rigid2D;
    Animator animator;
    float jumpForce = 680.0f;
    float walkForce = 30.0f;
    float maxWalkSpeed = 2.0f;
    float walkSpeed = 3.0f;

    int m_ReserveJump = 0;  //���� ����

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
        //���� ����
        if (Input.GetKeyDown(KeyCode.Space) == true)
        {
            m_ReserveJump = 3; //3 ������
        }

        // �����Ѵ�. -> float ������ �ذ��ؾ� ��
        //if(Input.GetKeyDown(KeyCode.Space) && this.rigid2D.velocity.y == 0) 
        if((-0.01f <= this.rigid2D.velocity.y && this.rigid2D.velocity.y <= 0.01f))
        {
            //3������ �� Ȯ����
            if (0 < m_ReserveJump)
            {
                this.animator.SetTrigger("JumpTrigger");
                this.rigid2D.velocity = new Vector2(rigid2D.velocity.x, 0.0f);
                this.rigid2D.AddForce(transform.up * this.jumpForce);

                m_ReserveJump = 0;
            }
        }
        //Update�Լ� ȣ�⸶�� ������
        if (0 < m_ReserveJump)
            m_ReserveJump--;

        // �¿� �̵�
        int key = 0;
        if (Input.GetKey(KeyCode.RightArrow)) key = 1;
        if (Input.GetKey(KeyCode.LeftArrow)) key = -1;

        // �÷��̾��� �ӵ�
        float speedx = Mathf.Abs(this.rigid2D.velocity.x);

        //// ���ǵ� ����
        //if(speedx < this.maxWalkSpeed)
        //{
        //    this.rigid2D.AddForce(transform.right * key * this.walkForce);
        //}

        //ĳ���� �̵�
        rigid2D.velocity = new Vector2((key * walkSpeed), rigid2D.velocity.y);

        // �����̴� ���⿡ ���� �����Ѵ�.
        if(key != 0)
        {
            transform.localScale = new Vector3(key, 1, 1);
        }

        // �÷��̾� �ӵ��� ���� �ִϸ��̼� �ӵ��� �ٲ۴�.
        if (this.rigid2D.velocity.y == 0)
        {
            this.animator.speed = speedx / 2.0f;
        }
        else
        {
            this.animator.speed = 1.0f;
        }

        // �÷��̾ ȭ�� ������ �����ٸ� ó������
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
            Debug.Log("��");
            SceneManager.LoadScene("ClearScene");
        }
        else if (coll.gameObject.name.Contains("water") == true)
        {
            Die();
        }
        else if(coll.gameObject.name.Contains("arrow") == true)
        {
            //��Ʈ �ϳ� ����
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
            //hp ���� ȸ��
            if(m_OverlapBlock != coll.gameObject)
            {
                hp += 0.5f; //hp�� ��Ʈ ��ĭ ȸ����

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
        //���� ���� ó�� �ʿ�
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

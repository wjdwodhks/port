using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerManagement : MonoBehaviour
{
    public Player_Data Pdata;   // 플레이어 데이터
    public Image hp_bar;        // 체력 바

    private Animator ani;       // 플레이어 애니메이션
    private Rigidbody2D rigid;  // 플레이어 물리엔진(동작)
    private BuffMgr buffManager;// 버프

    public bool isJump = false; // 점프 판별
    float curTime;
    public float att = 100f;  // 기본 공격력
    float creatt;   // 크리티컬 공격력

    // 알림
    public Text Noti;


    void Awake()
    {
        buffManager = GetComponent<BuffMgr>();
        rigid = GetComponent<Rigidbody2D>();
        ani = GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        if (GameManager.instance.isPlay == false) return;
        Move();
        Attack();
        nowHP();
    }
    public void Move()
    {
        float h = Input.GetAxisRaw("Horizontal");   // x축

        rigid.velocity = new Vector2(h * Pdata.speed, rigid.velocity.y);

        if (h > 0)
        {
            transform.localRotation = Quaternion.Euler(0, 0, 0);
            ani.SetBool("isMove", true);
        }
        else if (h < 0)
        {
            transform.localRotation = Quaternion.Euler(0, 180, 0);
            ani.SetBool("isMove", true);
        }
        else
            ani.SetBool("isMove", false);

        if (Input.GetKey(KeyCode.Space) && !isJump)
        {
            isJump = true;
            rigid.velocity = Vector2.up * Pdata.jumpPower;
            //anime.SetBool("isJumping", true);
            //anime.SetTrigger("doJumping");
        }
    }
    public Collider2D[] hitColliders;
    public void Attack()
    {
        bool isAttack = false;
        curTime += Time.deltaTime;
        float attackReach = 3.0f;
        
        hitColliders = Physics2D.OverlapCircleAll(this.transform.position, attackReach);
        //, LayerMask.GetMask("Monster")를 안쓰니까 해결이 되네,, 왜지..?
        if (Input.GetMouseButton(0) && curTime  > 0.5f - (Pdata.dex * 0.001f) && !isAttack)
        {
            curTime = 0;
            isAttack = true;
            ani.SetFloat("AttackSpeed", 1 + Pdata.dex * 0.001f);
            ani.SetBool("Attack", true);
            ani.SetTrigger("AttackTri");

            print("얍");

            foreach(Collider2D hitCollider in hitColliders)
            {
                print(hitCollider.transform.name);
                EnemyManagement enemy = hitCollider.GetComponent<EnemyManagement>();
                if(enemy != null)
                {
                    if(!enemy.Die())
                    {
                        print("공격 닿음");
                        int creRan = Random.Range(1, 10);
                        if(creRan<(Pdata.luck*0.001f))
                        {
                            print("특수 공격");
                            creatt = att * 2;
                            enemy.CreDamage(creatt);
                        }
                        else
                        {
                            enemy.Damage(att);
                            print("일반 공격");
                        }
                    }
                    else if (enemy.Die())
                    {
                        buffManager.ApplyBuff("Str", 10.0f, 1.5f);
                    }
                }
            }
            
        }
        else
        {
            ani.SetBool("Attack", false);
            isAttack = false;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        isJump = false;
    }
    public void nowHP()
    {
        hp_bar.fillAmount = Pdata.Hp / Pdata.MaxHp;
    }
    public void Damage(float Attack)
    {
        Pdata.Hp -= Attack - (Pdata.def / 1000); //뎀감
        if (Pdata.Hp <= 0)
        {
            Noti.text = "체력이 0이 되어 스탯이 일부 하락합니다.";
            StartCoroutine(Fadeout());
        }
        else if (Pdata.Hp == Pdata.Hp * 0.3f)
        {
            Noti.text = "체력이 부족합니다.";
            StartCoroutine(Fadeout());
        }
        else
        {
            Noti.text = "";
        }
    }
    IEnumerator Fadeout()
    {
        Noti.gameObject.SetActive(true);
        Noti.color = new Color(Noti.color.r, Noti.color.g, Noti.color.b, 1);
        while (true)
        {
            Noti.color = new Color(Noti.color.r, Noti.color.g, Noti.color.b, Noti.color.a - (Time.deltaTime / 2.0f));
            yield return null;
            if (Noti.color.a < 0.0f)
            {
                Noti.gameObject.SetActive(false);
            }
        }
    }
    public void CreDamage(long creatD)
    {
        Pdata.Hp -= creatD;

        // 데미지 텍스트 처리
    }
    //public List<Buff> onBuff = new List<Buff>();
    //public float pBuffChange(string type, float origin)
    //{
    //    if(onBuff.Count>0)
    //    {
    //        float temp = 0;
    //        for(int i = 0; i<onBuff.Count;i++)
    //        {
    //            if (onBuff[i].type.Equals(type))
    //            {
    //                temp += origin * onBuff[i].percentage;
    //            }
    //        }
    //        return origin + temp;
    //    }
    //    else
    //    {
    //        return origin;
    //    }
    //}
    //public float mBuffChange(string type, float origin)
    //{
    //    if (onBuff.Count > 0)
    //    {
    //        float temp = 0;
    //        for (int i = 0; i < onBuff.Count; i++)
    //        {
    //            if (onBuff[i].type.Equals(type))
    //                temp += origin * onBuff[i].percentage;
    //        }
    //        return origin = temp;
    //    }
    //    else
    //    {
    //        return origin;
    //    }
    //}
    //public void ChooseBuff(string type)
    //{
    //    switch (type)
    //    {
    //        case "atk":
    //            att = (long)pBuffChange(type, att);
    //            break;
    //        case "dex":
    //            Pdata.dex = (long)pBuffChange(type, Pdata.dex);
    //            break;
    //    }
    //}
    //public void minusBuff(string type)
    //{
    //    switch (type)
    //    {
    //        case "atk":
    //            att = (long)mBuffChange(type, att);
    //            break;
    //        case "dex":
    //            Pdata.dex = (long)mBuffChange(type, Pdata.dex);
    //            break;
    //    }
    //}
}

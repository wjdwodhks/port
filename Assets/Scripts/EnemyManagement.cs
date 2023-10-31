using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManagement : MonoBehaviour
{
    enum State
    {
        Idle,
        Move,
        Attack,
        Damaged,
        Die
    }

    public Enemy_Data m_Data;
    public GameObject Money;
    public GameObject player;
    public Transform target;

    State m_State;
    Transform playerPos;
    Vector3 StartPos;

    float findDistance = 10f;
    float attackDistance = 3f;

    float curTime;
    float attackDelay = 0.5f;
    void Start()
    {
        StartPos = this.transform.position;
        m_Data.maxHP = m_Data.hp;
        playerPos = GameObject.FindGameObjectWithTag("Player").transform;
        m_State = State.Idle;
    }
    public float monsterHP;
    void Update()
    {
        monsterHP = m_Data.hp;
        if (GameManager.instance.isPlay == false) return;
        switch (m_State)
        {
            case State.Idle:
                Idle();
                break;
            case State.Move:
                Move();
                break;
            case State.Attack:
                Attack();
                break;
            case State.Damaged:
                break;
            case State.Die:
                break;
        }
    }
    void Idle()
    {
        if (Vector3.Distance(playerPos.position, this.transform.position) < findDistance)
        {
            m_State = State.Move;
        }
    }
    void Move()
    {
        Vector3 destination = playerPos.position - this.transform.position;
        destination.Normalize();

        transform.position += destination * m_Data.moveSpeed * Time.deltaTime;
        if (Vector3.Distance(playerPos.position, this.transform.position) < attackDistance)
        {
            m_State = State.Attack;
            curTime = attackDelay;
        }
    }
    void Attack()
    {
        if (Vector3.Distance(playerPos.position, this.transform.position) < attackDistance)
        {
            curTime += Time.deltaTime;

            if (curTime >= attackDelay)
            {
                player.GetComponent<PlayerManagement>().Pdata.Hp -= m_Data.power;
                curTime = 0;
                print(player.GetComponent<PlayerManagement>().Pdata.Hp);
            }
        }
        else
        {
            m_State = State.Move;
        }
    }
    public void Damage(float att)
    {
        print("¥Í¿Ω");
        m_Data.hp -= att;
        if (m_Data.hp <= 0)
        {
            m_State = State.Die;
        }
        Damaged();
    }
    public void CreDamage(float att)
    {
        m_Data.hp -= att;
        if (m_Data.hp <= 0)
        {
            m_State = State.Die;
        }
        Damaged();
    }
    void Damaged()
    {
        StartCoroutine(DamageProcess());

    }
    public bool Die()
    {
        StopAllCoroutines();
        StartCoroutine(DieProcess());
        if (m_Data.hp > 0)
            return false;
        else
        {
            return true;
        }
    }
    IEnumerator DamageProcess()
    {
        yield return new WaitForSeconds(0.5f);
        m_State = State.Move;
    }
    IEnumerator DieProcess()
    {
        int randCount = Random.Range(5, 10);
        for (int i = 0; i < randCount; ++i)
        {
            Vector3 screenPos = Camera.main.WorldToScreenPoint(transform.position);
            GameObject itemFx = Instantiate(Money, screenPos, Quaternion.identity);
            itemFx.transform.SetParent(GameObject.Find("Canvas").transform);
            itemFx.GetComponent<ItemFx>().Explosion(screenPos, target.position, 150f);
        }
        yield return new WaitForSeconds(2f);
        GameManager.instance.SetMoney(Random.Range(50, 100));

        Destroy(gameObject);
        transform.position = StartPos;
    }
}

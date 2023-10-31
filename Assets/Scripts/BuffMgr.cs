using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class BuffMgr : MonoBehaviour
{
    public PlayerManagement player;
    private void Start()
    {
        player = GetComponent<PlayerManagement>();
    }
    private List<Buff> activeBuffs = new List<Buff>();
    public void ApplyBuff(string buffName,float duration, float effectValue)
    {
        // �ߺ��� ������ �ִ��� Ȯ���ϰ�, �ִٸ� ���� �ð��� ����
        foreach (Buff buff in activeBuffs)
        {
            if(buff.buffName == buffName)
            {
                buff.duration = duration;
                return;
            }
        }
        // ��ø�� ������ ������ ���ο� ������ �߰�
        activeBuffs.Add(new Buff(buffName, duration, effectValue));
        if(buffName == "Str")
        {
            player.att += 2;
        }
    }
    private void Update()
    {
        for(int i = activeBuffs.Count-1;i>=0;i--)
        {
            activeBuffs[i].duration -= Time.deltaTime;
            if(activeBuffs[i].duration<=0)
            {
                // ������ ����Ǹ� ����
                activeBuffs.RemoveAt(i);
            }
        }
    }
}

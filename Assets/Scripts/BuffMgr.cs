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
        // 중복된 버프가 있는지 확인하고, 있다면 지속 시간을 갱신
        foreach (Buff buff in activeBuffs)
        {
            if(buff.buffName == buffName)
            {
                buff.duration = duration;
                return;
            }
        }
        // 중첩된 버프가 없으면 새로운 버프를 추가
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
                // 버프가 종료되면 제거
                activeBuffs.RemoveAt(i);
            }
        }
    }
}

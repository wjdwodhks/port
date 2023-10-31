using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData", menuName = "Data/PlayerData", order = 0)]
public class Player_Data : ScriptableObject
{
    public string Pname;
    public float speed;
    public float jumpPower;
    public float Hp;
    public float MaxHp;
    public int Mp;
    public int MaxMp;
    public float def;
    public float str;
    public float dex;
    public float luck;
    public float Int;
    public bool isLive;
}

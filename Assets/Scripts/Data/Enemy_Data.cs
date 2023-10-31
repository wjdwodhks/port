using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyData", menuName = "Data/EnemyData", order = 2)]
public class Enemy_Data : ScriptableObject
{
    public float hp;
    public float maxHP;
    public float power;
    public float moveSpeed;
}

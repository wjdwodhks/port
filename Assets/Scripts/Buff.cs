using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Buff : MonoBehaviour
{
    public string buffName;
    public float duration;
    public float effentValue;

    public Buff(string name,float dur,float value)
    {
        buffName = name;
        duration = dur;
        effentValue = value;
    }
}

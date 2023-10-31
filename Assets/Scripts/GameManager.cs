using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public bool isPlay;

    public float Money = 1000;
    public Text MoneyTxt;

    public Player_Data p_Data;

    private void Awake()
    {
        instance = this;
        SetMoney(Money);

        p_Data.name = DataManager.instance.nowPlayer.name;
        MoneyTxt.text = DataManager.instance.nowPlayer.coin.ToString();
    }

    public void SetMoney(float money)
    {
        Money += money;
        StartCoroutine(Count(Money, Money - money));
    }
    // ���� ī���� �ִϸ��̼�
    IEnumerator Count(float target, float current)
    {
        float duration = 0.5f;  // ī���ÿ� �ɸ��� �ð�
        float offset = (target - current) / duration;
        while (current < target)
        {
            current += offset * Time.deltaTime;
            MoneyTxt.text = string.Format("{0:n0}", (int)current);
            yield return null;
        }
        current = target;
        MoneyTxt.text = string.Format("{0:n0}", (int)current);
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            DataManager.instance.SaveData();
            print("����!");
        }
    }
}

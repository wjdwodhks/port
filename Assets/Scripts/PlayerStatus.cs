using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStatus : MonoBehaviour
{
    public Player_Data Pdata;   // �÷��̾� ������
    
    // ����
    public GameObject status;
    public Text HpTxt;
    public Text DefTxt;
    public Text StrTxt;
    public Text DexTxt;
    public Text LuckTxt;
    public Text IntTxt;
    void Update()
    {
        openStat();
    }
    public void openStat()
    {
        if (Input.GetKey(KeyCode.J))
        {
            status.gameObject.SetActive(true);
        }
    }
    public void downUI()
    {
        status.gameObject.SetActive(false);
    }
    public void HpUP()
    {
        if (GameManager.instance.Money >= 1000)
        {
            GameManager.instance.SetMoney(-1000);
            Pdata.MaxHp += 10f;
            Pdata.Hp += 10f;
            HpTxt.text = "" + Pdata.MaxHp;
        }
        else
        {
            print("����� �� ����");
        }
    }
    public void DefUP()
    {
        if (GameManager.instance.Money >= 1000)
        {
            GameManager.instance.SetMoney(-1000);
            Pdata.def += 10f;
            DefTxt.text = "" + Pdata.def;
        }
        else
        {
            print("����� �� ����");
        }
    }
    public void StrUP()
    {
        if (GameManager.instance.Money >= 1000)
        {
            GameManager.instance.SetMoney(-1000);
            Pdata.str += 10f;
            StrTxt.text = "" + Pdata.str;
        }
        else
        {
            print("����� �� ����");
        }
    }
    public void DexUP()
    {
        if (GameManager.instance.Money >= 1000)
        {
            GameManager.instance.SetMoney(-1000);
            Pdata.dex += 10f;
            DexTxt.text = "" + Pdata.dex;
        }
        else
        {
            print("����� �� ����");
        }
    }
    public void LuckUP()
    {
        if (GameManager.instance.Money >= 1000)
        {
            GameManager.instance.SetMoney(-1000);
            Pdata.luck += 10f;
            LuckTxt.text = "" + Pdata.luck;
        }
        else
        {
            print("����� �� ����");
        }
    }
    public void IntUP()
    {
        if (GameManager.instance.Money >= 1000)
        {
            GameManager.instance.SetMoney(-1000);
            Pdata.Int += 10f;
            LuckTxt.text = "" + Pdata.Int;
        }
        else
        {
            print("����� �� ����");
        }
    }
}

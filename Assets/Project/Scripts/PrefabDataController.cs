using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class PrefabDataController : MonoBehaviour
{
    DataListEdit instance;
    public Text NumberUI;
    public InputField NameUI;
    public InputField AmountUI;
    public int Number;

    private void Awake()
    {
        instance = DataListEdit.Instance;
    }
    //生成+初始化UI數據
    //賦予本UIBlock編號
    //把Ori數據預填寫(如果是在原陣列大小裡才執行)
    void ItemInit(int num)
    {
        string strNum = (num + 1).ToString() + ".";
        NumberUI.text = strNum;
        Number = num;
        
        if (num < instance.ItemName.Count)
        {
            NameUI.text = instance.ItemName[num];
            AmountUI.text = instance.ItemAmount[num].ToString();
        }
    }
    public void ChangeItemName(string str)
    {
        instance.currItemName[Number] = str;
    }
    public void ChangeitemAmount(string num)
    {
        instance.currItemAmount[Number] = Int32.Parse(num);
    }
    void DestorySelf()
    {
        Destroy(this.gameObject);
    }
}

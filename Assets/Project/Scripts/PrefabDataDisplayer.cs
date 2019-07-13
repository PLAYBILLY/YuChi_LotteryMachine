using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PrefabDataDisplayer : MonoBehaviour
{
    public MyData data;
    public Text NumberUI;
    public Text ItemNameUI;
    public Text ItemAmountUI;
    public Text ItemLastAmountUI;
    public int number;
    bool firstLoad = false;
    //將資料Post
    // Start is called before the first frame update
    void Start()
    {
        ItemInit(number);
            DataDisplay.Instance.OnDisplayClicked += DisplayLastAmount;
            DataDisplay.Instance.OnDataUpdate += DataUpdate;
        firstLoad = true;
        Debug.Log("DoItem");
    }
    /*
    private void OnDisable()
    {
        DataDisplay.OnDisplayClicked -= DisplayLastAmount;
        DataDisplay.OnDataUpdate -= DataUpdate;
    }
    private void OnEnable()
    {
        if (firstLoad == true)
        {
            DataDisplay.OnDisplayClicked += DisplayLastAmount;
            DataDisplay.OnDataUpdate += DataUpdate;
        }
    }
    */
    void ItemInit(int num)
    {
        string strNum = (num + 1).ToString() + ".";
        NumberUI.text = strNum;//標編號
        ItemNameUI.text = data.ItemName[num];//給名字
        ItemAmountUI.text = data.ItemOriAmount[num].ToString();//給數量
        ItemLastAmountUI.text = data.ItemAmount[num].ToString();
    }
    void DisplayLastAmount()
    {
        ItemLastAmountUI.enabled = !ItemLastAmountUI.enabled;
    }

    void DataUpdate()
    {
        ItemLastAmountUI.text = data.ItemAmount[number].ToString();

    }
}

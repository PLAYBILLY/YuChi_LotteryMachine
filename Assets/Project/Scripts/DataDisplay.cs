using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DataDisplay : MonoBehaviour
{
    public static DataDisplay Instance;
    public MyData data;
    public GameObject PrefabObj;
    public Transform TargetParent;
    public LotteryDrawManage LotteryScript;
    public delegate void ClickAction();
    public event ClickAction OnDisplayClicked;
    public event ClickAction OnDataUpdate;
    bool FirstLoad = false;
    bool isLastAmountDisplay = false;

    //三種資料
    /// <summary>
    /// ItemName
    /// ItemAmount
    /// ItemLastAmount
    /// </summary>
    //兩個按鈕
    //ResetButton
    //LastAmount
    //將SO的資料放入Text中
    //動態生成
    private void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        Init();
    }
    void Init()
    {
        //生成資料
        for (int i = 0; i < data.Length; i++)
        {
            GameObject Obj = Instantiate(PrefabObj, Vector3.zero, Quaternion.Euler(Vector3.zero), TargetParent);
            Obj.GetComponent<PrefabDataDisplayer>().number = i;
        }
        FirstLoad = true;
    }
    private void OnEnable()
    {
        if (FirstLoad)
        {
            DataUpdate();
        }
    }
    //剩餘數量數據更新
    public void DataUpdate()
    {
        OnDataUpdate();
    }
    //顯示剩餘數量
    public void OnDisplayButton()
    {
        OnDisplayClicked();
        isLastAmountDisplay = !isLastAmountDisplay;
    }
    //重置數量至原本數
    public void OnClearData()
    {
        for (int i = 0; i < data.Length; i++)
        {
            data.ItemAmount[i] = data.ItemOriAmount[i];
        }
        OnDataUpdate();
        SaveData.Instance.ListSave();
        LotteryScript.ResetRandomData();
    }
    private void OnDisable()
    {
        if (isLastAmountDisplay == true)
        {
            OnDisplayButton();
        }
    }

}

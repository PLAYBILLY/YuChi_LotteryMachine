using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using DG.Tweening;
using System;
/// <summary>
/// 編輯資料庫頁面UI
/// 要在這邊作個資料暫存區
/// 等User點擊儲存時，才會匯入ScriptableObj跟Data裡
/// 並且設定這個儲存資料DataX為當前currAmount
/// 功能：
/// 1.輸入數字欄位，打開對應陣列空間
/// 2.打開陣列空間，賦予流水號，設置每個欄位對應要儲存的目標UI
/// 3.
/// </summary>
/// 
public class DataListEdit : MonoBehaviour
{
    public static DataListEdit Instance;
    public MyData data;
    public Scrollbar Scroll;
    public InputField ItemLengthUI;
    public InputField DataNameUI;
    public GameObject PrefabObj;
    public Transform TargetParent;
    public Text TittleUI;

    public bool isNewData = true;

    public int currListAmount;//當前資料大小
    public List<string> currItemName;
    public List<int> currItemAmount;
    public List<string> ItemName;
    public List<int> ItemAmount;
    public List<GameObject> DataObj;

    private void Awake()
    {
        Instance = this;
    }
    private void OnEnable()
    {
        
    }
    void Start()
    {
        Initalization();//+Dataload()+ChangeListamount(currListAmount)+ListCreat()*currListAmount;
    }
    //初始化data資料給本地端
    void Initalization()
    {
        //測試讀取功能
        //SaveData.Instance.ListLoad();
        
        //判斷是否為新的資料
        if (data.Length == 0) isNewData = true;
        else isNewData = false;

        TittleUI.text = data.DataName;
        ItemName.Clear();
        ItemAmount.Clear();
        for (int i = 0; i < data.Length; i++)
        {   
            ItemName.Add(data.ItemName[i]);
            ItemAmount.Add(data.ItemOriAmount[i]);
        }
        //生成陣列
        DataLoad();//ChangeListamount(currListAmount)+ListCreat()*currListAmount;
        
    }

    public void DataLoad()
    {
        //告知陣列大小
        ItemLengthUI.text = data.Length.ToString();
        currListAmount = data.Length;
        //將Ori資料放入curr中，ListCreat()是抓Curr為主

        //依陣列大小生成對應數量，並偵測Data是否有資料，如果有，貼上數據
        ChangeListamount(currListAmount);//ListCreat()*currListAmount;

    }
    //陣列生成
    void ListCreat()
    {
        //if (currListAmount == amount) { Debug.Log("same amount");return; }

        //變數上的陣列先生成
            GameObject Obj = Instantiate(PrefabObj, Vector3.zero, Quaternion.Euler(Vector3.zero), TargetParent);
            Obj.SendMessage("ItemInit", DataObj.Count);
            DataObj.Add(Obj);
        currItemAmount.Add(0);
        currItemName.Add("");
        if (DataObj.Count >= currListAmount) { CancelInvoke(); Scroll.value = 1; }
    }
#if UNITY_EDITOR
    //測試用
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z)) 
        {
            GameObject Obj = Instantiate(PrefabObj, Vector3.zero, Quaternion.Euler(Vector3.zero), TargetParent);
            Obj.SendMessage("ItemInit", DataObj.Count);
            DataObj.Add(Obj);
            currItemAmount.Add(0);
            currItemName.Add("");
            Scroll.value = 1;
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            for (int i = 0; i < DataObj.Count; i++)
            {
                DataObj[i].SendMessage("DestorySelf");
            }
            DataObj.Clear();
        }

    }
#endif
    //每次更改完數量之後，重製List
    public void ChangeListamount(string num)
    {
        if (currListAmount == Int32.Parse(num)) { Debug.Log("same amount"); return; }
        for (int i = 0; i < DataObj.Count; i++)
        {
            DataObj[i].SendMessage("DestorySelf");
        }
        DataObj.Clear();
        ClearList();
        currListAmount = Int32.Parse(num);
        InvokeRepeating("ListCreat", 0, 0.01f);
    }
    public void ChangeListamount(int num)
    {
        if (num <= 0) { Debug.Log("請輸入大於0以上的數字");return; }

        for (int i = 0; i < DataObj.Count; i++)
        {
            DataObj[i].SendMessage("DestorySelf");
        }
        DataObj.Clear();
        ClearList();
        currListAmount = num;
        InvokeRepeating("ListCreat", 0, 0.01f);
    }
    //儲存，先不要動到存檔機制
    public void SaveListToData()
    {
        //如果有空的數據，填上暫時資料
        for (int i = 0; i < currListAmount; i++)
        {
            if (i>= ItemName.Count)
            {
                ItemName.Add("");
                ItemAmount.Add(0);
            }
        }
        for (int i = 0; i < currListAmount; i++)
        {
            //當輸入欄是空的，套用舊的資料
            if ((currItemName[i] == null || currItemName[i] == "") && ItemName[i] != null )
            {
                currItemName[i] = ItemName[i];
            }
            if (currItemAmount[i]  == 0 && ItemAmount[i] != 0)
            {
                currItemAmount[i] = ItemAmount[i];
            }
            ItemName[i] = currItemName[i];
            ItemAmount[i] = currItemAmount[i];
        }
        if (currListAmount >0)
        {
            isNewData = false;
        }
        SaveToFile();
    }
    //將已經存好的Data餵給SO，並呼叫執行儲存
    void SaveToFile()
    {
        data.ItemName.Clear();
        data.ItemAmount.Clear();
        data.ItemOriAmount.Clear();
        data.Length = currListAmount;
        data.DataName = TittleUI.text;
        
        for (int i = 0; i < data.Length; i++)
        {
            data.ItemName.Add(ItemName[i]);
            data.ItemAmount.Add(ItemAmount[i]);
            data.ItemOriAmount.Add(ItemAmount[i]);
        }
        //把Item陣列的值覆寫進SO，執行儲存
        SaveData.Instance.ListSave();
        //FileData也要存資料
        SaveData.Instance.FileDataSave();
    }

    void ClearList()
    {
        currItemAmount.Clear();
        currItemName.Clear();

    }
    //更改名稱
    public void SetDataName(string str)
    {

        TittleUI.text = str;
        //data.DataName = str;
    }


}

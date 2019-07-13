using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using CI.QuickSave;
using Sirenix.OdinInspector;

public class SaveData : MonoBehaviour {
    [Header("遊戲中顯示用Class")]
    public MyData data;
    [Header("存檔數量")]
    public int fileAmount;
    [Header("當前存檔號")]
    public int currAmount;
    [Header("流水檔案，以Data+數字為命名規則")]
    public List<string> fileNames;
    [Header("User可編輯之檔案名稱")]
    public List<string> dataNames;
    [Header("資料是否已被建置")]
    public List<bool> isCreated;
    public static SaveData Instance;
    //SaveData主要與ScriptableObj作互動，其他腳本也與SO互動，本腳本的讀/寫將會另外給特定時機、按鈕中觸發
    /// <summary>
    /// FileData擁有變數：
    /// fileAmount檔案數量 = 固定10筆 =>改成已建檔的數量(?)
    /// currAmount當前正在處理的檔案別
    /// fileNames檔案流水名稱(Data0~Data9)
    /// dataNames檔案對應的資料名稱(User自定義)
    /// isCreated當前所有檔案是否已建檔
    /// 
    /// 
    /// Data擁有變數：
    /// DataName
    /// Length
    /// ItemName
    /// ItemAmount
    /// ItemOriAmount
    /// </summary>
    /// 要固定存檔數量
    /// 最大10個
    /// 用陣列管理所有存檔檔案的狀態
    /// 狀態會有：1.是否建檔2.是否有資料3.檔案名稱
    /// 序列化：先抓取有哪些DataNum的狀態是isCreated的，放進顯示陣列，再生成這些檔案，讀取DataName+顯示於欄位
    /// 讀取：讀取這個檔案
    /// 
    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        //FirstSerializeField();
        if (!File.Exists(Application.streamingAssetsPath + "/QuickSave/FileData.Json")) FirstSerializeField();
        InitalizationSaveData();
        //測試儲存陣列的結果

    }
    //第一次建檔
    public void FirstSerializeField()
    {
        //生成10筆資料
        fileAmount = 0;
        currAmount = 0;
        for (int i = 0; i < 10; i++)
        {
            fileNames.Add("Data" + i);
            dataNames.Add("New Data");
            isCreated.Add(false);
        }

        //先存一次預設值
        FileDataSave();
        //還要做10筆資料的預先建置嗎?
    }
    //初始化
    //讀取存檔資料
    public void InitalizationSaveData()
    {
        //防呆
        string dirPath = Application.streamingAssetsPath + "/QuickSave/FileData.Json";
        if (!File.Exists(dirPath)) { Debug.Log("missed file"); return; }
        //讀取DataName
        QuickSaveReader reader = QuickSaveReader.Create("FileData");
        fileAmount = reader.Read<int>("fileAmount");
        currAmount = reader.Read<int>("currAmount");
        isCreated = reader.Read<List<bool>>("isCreated");
        dataNames = reader.Read < List<string>>("dataName");
        fileNames = reader.Read<List<string>>("fileName");

        Debug.Log("Data Initalization");
    }
    //將當前存檔資訊存給FileData
    public void FileDataSave()
    {
        //將當前dataName寫入fileName裡中
        QuickSaveWriter writer = QuickSaveWriter.Create("FileData");
        dataNames[currAmount] = data.DataName;
        writer.Write("fileAmount", fileAmount);
        writer.Write("currAmount", currAmount);
        writer.Write("isCreated", isCreated);
        writer.Write("dataName", dataNames);
        writer.Write("fileName", fileNames);
        writer.Commit();
    }
    //新增檔案
    public void FileDataAdd(string newData)
    {

        //除了FileData要新增一筆檔案資料以外
        //dataNames.Add(newData);
        //fileNames.Add("Data" + fileAmount);
        //用迴圈尋找當前的空檔區
        for (int i = 0; i < 10; i++)
        {
            if (isCreated[i] == false)
            {
                isCreated[i] = true;
                currAmount = i;
                break;
            }
        }

        fileAmount++;
        FileDataSave();
        //還要將SO清除，並且製作一份Data_Num的檔案進行關聯
        data.ItemAmount.Clear();
        data.ItemName.Clear();
        data.ItemOriAmount.Clear();
        data.Length = 0;
        data.DataName = newData;
        ListSave();
        //因此，需要連頁面都跳轉到編輯去(預設名稱為New Data)
        MySceneManager.Instance.OnSceneChange((int)SceneName.EditData);
    }

    //2.
    //讀取序列化，
    //儲存序列化，將把陣列作成一大堆string
    public void ListSave()
    {
        //儲存項目名稱與數量
        QuickSaveWriter writer = QuickSaveWriter.Create("Data" + currAmount);
        writer.Write("DataName", data.DataName);
        writer.Write("Length", data.Length);
        
        for (int i = 0; i < data.Length; i++)
        {
            writer.Write(i.ToString(), data.ItemName[i]);
            writer.Write(data.ItemName[i], data.ItemAmount[i]);
            writer.Write("Ori" + data.ItemName[i], data.ItemOriAmount[i]);
        }
        
        writer.Commit();
    }
    public void ListLoad()
    {
        //防呆
        string fileStr = "Data" + currAmount;
        string dirPath = Application.streamingAssetsPath + "/QuickSave/" + fileStr +".Json";
        if (!File.Exists(dirPath)) { Debug.Log("missed file"); return; }
        
        //清空當前Class紀錄
        data.ItemName.Clear();
        data.ItemAmount.Clear();
        data.ItemOriAmount.Clear();
        //讀取DataName
        QuickSaveReader reader = QuickSaveReader.Create(fileStr);
        data.DataName = reader.Read<string>("DataName");
        data.Length = reader.Read<int>("Length");
        for (int i = 0; i < data.Length; i++)
        {
            data.ItemName.Add(reader.Read<string>(i.ToString()));
            data.ItemAmount.Add(reader.Read<int>(data.ItemName[i]));
            data.ItemOriAmount.Add(reader.Read<int>("Ori" + data.ItemName[i]));
        }
        
        Debug.Log("Data Loaded");
    }
    //刪Data檔案
    public void ListDelet()
    {
        //防呆
        string fileStr = "Data" + currAmount;
        string dirPath = Application.streamingAssetsPath + "/QuickSave/" + fileStr + ".Json";

        if (File.Exists(dirPath))
        {
            File.Delete(dirPath);
            Debug.Log(fileStr + "is been deleted");
        }
    }
    public void ListDelet(int num)
    {      
        //防呆
        string fileStr = "Data" + num;
        string dirPath = Application.streamingAssetsPath + "/QuickSave/" + fileStr + ".Json";
        isCreated[num] = false;
        dataNames[num] = "New Data";
        fileAmount--;
        FileDataSave();
        if (File.Exists(dirPath))
        {
            File.Delete(dirPath);
            Debug.Log(fileStr + "is been deleted");
        }
        
    }
}

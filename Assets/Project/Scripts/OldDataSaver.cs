using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using UnityEditor;

public class OldDataSaver : MonoBehaviour {

    public static OldDataSaver instance;
    public MyData data;
    
    public Text Testtext;
    public Text Testtext2;
    public int[] ChangeData;

    private void Awake()
    {
        instance = this;
    }

    void Start ()
    {
        
        PlayerPrefsInitialization();
        LoadPlayerPrefs();
        DisplayData();

    }
    public void SaveData()
    {
        MyData tdata = ScriptableObject.CreateInstance<MyData>();
        List<int> QQ = new List<int>();
        QQ.Add(10);
        QQ.Add(20);

        tdata.ItemAmount.Add(QQ[0]);
        tdata.ItemAmount.Add(QQ[1]);

       // AssetDatabase.CreateAsset()

    }
    public void SetDataCount(string str)
    {
        //data.ItemAmount[0] -= 1;
        SavePlayerPrefs(str);
        LoadPlayerPrefs();
        DisplayData();

    }

    void DisplayData()
    {
        Testtext.text = "";
        Testtext2.text = "";
        for (int i = 0; i < data.ItemName.Count; i++)
        {
            Testtext.text += "Ori " + data.ItemName[i] + "：" + data.ItemAmount[i] + "\n";
            
        }
        for (int i = 0; i < data.ItemName.Count; i++)
        {
            Testtext2.text += "Methf " + data.ItemName[i] + "：" + (data.ItemAmount[i] - PlayerPrefs.GetInt(data.ItemName[i].ToString())) + "\n";
        }

    }

    //初始化
    void PlayerPrefsInitialization()
    {
        for (int i = 0; i < data.ItemName.Count; i++)
        {
            if (!PlayerPrefs.HasKey(data.ItemName[i]))
            {
                PlayerPrefs.SetInt(data.ItemName[i], 0);
            }
        }
        
    }


    public void SavePlayerPrefs(string str)
    {
        PlayerPrefs.SetInt(str, PlayerPrefs.GetInt(str) + 1);
    }

    public void LoadPlayerPrefs()
    {
        for (int i = 0; i < data.ItemName.Count; i++)
        {
            ChangeData[i] = PlayerPrefs.GetInt(data.ItemName[i]);
        }
        DisplayData();
    }

    public void CleanPlayerPrefs()
    {
        PlayerPrefs.DeleteAll();
        LoadPlayerPrefs();
    }



}

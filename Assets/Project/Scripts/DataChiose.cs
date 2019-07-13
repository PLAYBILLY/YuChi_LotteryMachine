using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// 資料選擇頁面
/// 1.進入本頁時，自動生成所有已經存好的紀錄資料(需要跟SaveData要資料)
/// 2.選擇高亮顯示，並彈出對應按鈕
/// 按鈕"Play"：複寫對應資料到SO上，進入Main場景
/// 按鈕"Edit"：複寫對應資料到SO上，進入Edit場景
/// 按鈕"Delete"：刪除對應資料，清空當前選取
/// 按鈕"New"：新增資料，副寫空資料到SO上，進入Edit場景
/// 
/// </summary>
public class DataChiose : MonoBehaviour
{
    public List<GameObject> DataObj;
    public GameObject PrefabObj;
    public Transform TargetParent;
    public SaveData data;
    //public Button.ButtonClickedEvent _event;
    public delegate void DelectAction(int num);
    public static DelectAction OnDelect;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("Initalization", 0.1f,0.01f);
    }
    void Initalization()
    {
        //變數上的陣列生成
        if (data.fileAmount <= 0) return;
        
        Create();
        if (DataObj.Count >= data.fileAmount) CancelInvoke();
    }
    void Create(string _name = null)
    {
        //生成
        GameObject Obj = Instantiate(PrefabObj, Vector3.zero, Quaternion.Euler(Vector3.zero), TargetParent);
        //賦予編號
        Obj.GetComponent<PrefabDataFileController>().Number = DataObj.Count;

        //
        //Obj.GetComponentInChildren<Text>().text = data.dataNames[DataObj.Count];
        if (_name != null) Obj.GetComponentInChildren<Text>().text = _name;

        //存入陣列
        DataObj.Add(Obj);
    }
    public void OnNewButton()
    {
        Create("New Data");
        data.FileDataAdd("New Data");
    }
    public void ButtonTry()
    {
        Debug.Log("Pressed");
    }
    //當建檔資料達到10筆，關閉

}

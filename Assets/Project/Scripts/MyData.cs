using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyData : ScriptableObject {

    //public string ItemName;
    //public int ItemAmount;
    public string DataName;//項目名稱(可給User自定義)
    public int Length;//項目種類
    public List<string> ItemName;//項目名稱
    public List<int> ItemAmount;//項目數量
    public List<int> ItemOriAmount;//項目原數量
}

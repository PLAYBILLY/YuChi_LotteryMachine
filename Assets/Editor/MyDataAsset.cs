using UnityEngine;
using UnityEditor;

public class MyDataAsset
{
    [MenuItem("Assets/Create/My Data")]
    public static void CreateAsset()
    {
        ScriptableObjectUtility.CreateAsset<MyData>();
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class PrefabDataFileController : MonoBehaviour
{
    public int Number;
    public Text _text;
    public Button LoadButton;
    public Button EditButton;
    public Button DeleteButton;
    public Image DeleteFill;
    public bool isPressed;

    // Start is called before the first frame update
    void Start()
    {
        _text.text = SaveData.Instance.dataNames[Number];
        DeleteButton.onClick.AddListener(() => OnDelectButton());
        EditButton.onClick.AddListener(() => OnEditButton());
        LoadButton.onClick.AddListener(() => OnLoadButton());
        
    }

    public void OnLoadButton()
    {
        //如果檔案本身還是空的，不能給它執行

        //執行讀取檔案
        SaveData.Instance.currAmount = Number;
        SaveData.Instance.ListLoad();
        //切換到遊戲頁面
        MySceneManager.Instance.OnSceneChange((int)SceneName.Main);
    }
    public void OnEditButton()
    {
        //執行讀取檔案
        SaveData.Instance.currAmount = Number;
        SaveData.Instance.ListLoad();
        //切換到編輯頁面
        MySceneManager.Instance.OnSceneChange((int)SceneName.EditData);
    }
    public void OnDelectButton()
    {
        //按住5秒才會執行刪除
        if (DeleteFill.fillAmount >= 1)
        {
            //自刪，並且向數據庫告知
            SaveData.Instance.ListDelet(Number);
            Destroy(this.gameObject);
            Debug.Log("GG");
        }
        Debug.Log("NO GG");
        //Destroy(this.gameObject);
    }
    public void OnDeleteFillImage()
    {
        if (isPressed == true)
        {
            DeleteFill.fillAmount += 0.02f;
        }
    }
    public void IsPressedChange(bool boo)
    {
        isPressed = boo;
        if (DeleteFill.fillAmount < 1)
        {
            DeleteFill.fillAmount = 0;
        }
    }

}

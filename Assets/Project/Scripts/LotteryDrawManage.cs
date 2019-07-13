using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class LotteryDrawManage : MonoBehaviour {
    List<string> RandomNumbers = new List<string>();
    public GameObject _object;
    public Text _text;
    public MyData data;

    public bool ButtonLock = false;
    public bool CanDraw = false;
    public bool CanClose = false;
    public GameObject MassagePanel;
    public GameObject Key;
    public GameObject DrawPanel;

    //改寫執行方式
    private void Start()
    {
        //先把所有編號裝進陣列裡
        for (int i = 0; i < data.Length; i++)
        {
            for (int n = 0; n < data.ItemAmount[i]; n++)
            {
                RandomNumbers.Add(data.ItemName[i]);
                Debug.Log("呼叫陣列" + data.ItemName[i]);
            }
        }


    }
    public void ResetRandomData()
    {
        RandomNumbers.Clear();
        for (int i = 0; i < data.Length; i++)
        {
            for (int n = 0; n < data.ItemAmount[i]; n++)
            {
                RandomNumbers.Add(data.ItemName[i]);
                Debug.Log("重置陣列" + data.ItemName[i]);
            }
        }
    }
    //抽獎Button
    //
    public void DrawANumber()
    {
        //要解鎖可抽BOOL才能執行
        if (CanDraw == true && ButtonLock == false)
        {
            //防呆
            if (MassagePanel.activeSelf == true)
                MassagePanel.SetActive(false);
            //鑰匙表演
            Key.transform.DOKill();
            Key.transform.DOScale(0, 0.3f);
            //彈出得獎訊息
            MassagePanel.SetActive(true);
            ButtonLock = true;
            _object.transform.localScale = new Vector3(0, 0, 0);
            _object.transform.DOScale(1, 0.8f).SetEase(Ease.OutElastic).OnComplete(() => { ButtonLock = false; CanDraw = false; });

            //StopAllCoroutines();
            //StartCoroutine(DisplayMessage());
            //設置亂數的中獎物
            int randomNumber = Random.Range(0, RandomNumbers.Count);
            //先更改SO的值，再處理存檔
            for (int i = 0; i < data.Length; i++)
            {
                if (data.ItemName[i].Contains(RandomNumbers[randomNumber]))
                {
                    data.ItemAmount[i]--;
                }
            }
            SaveData.Instance.ListSave();
            _text.text = "你抽中了:\n" + RandomNumbers[randomNumber];
            //Debug.Log("你抽中了:" + RandomNumbers[randomNumber]);
            RandomNumbers.RemoveAt(randomNumber);
            _text.text += "\n" + "剩下" + RandomNumbers.Count + "個獎品";
            //Debug.Log("剩下" + RandomNumbers.Count + "個獎品");



        }

    }
    public void ClosePanel()
    {
        if (ButtonLock == false && CanClose == true)
        {
            MassagePanel.SetActive(false);
        }
    }

    //IEnumerator DisplayMessage()
    //{
    //    _object.SetActive(true);
    //    yield return new WaitForSeconds(2.5f);
    //    _object.SetActive(false);
    //}

    ///表演指令
    ///1.切換頁面
    ///2.按住K鍵解鎖抽獎紐
    ///3.按住L鍵解鎖關閉紐
    ///4.所有動畫皆須播放完後才可接收指令
    ///
    private void FixedUpdate()
    {
        if (ButtonLock == false)
        {
            if (Input.GetKeyDown(KeyCode.K))
            {
                Key.transform.DOKill();
                Key.transform.DOScale(1, 0.3f);
                CanDraw = true;
            }
            else if (Input.GetKeyUp(KeyCode.K))
            {
                Key.transform.DOKill();
                Key.transform.DOScale(0, 0.3f);
                CanDraw = false;
            }

            if (Input.GetKeyDown(KeyCode.L))
            {
                CanClose = true;
            }
            else if (Input.GetKeyUp(KeyCode.L))
            {
                CanClose = false;
            }
        }

        //切換自兩邊?
        if (Input.GetKeyDown(KeyCode.J))
        {
            DrawPanel.SetActive(!DrawPanel.activeSelf);
        }
    }
}

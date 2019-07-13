using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public enum SceneName
{
Menu,
ChioseData,
EditData,
Roll,
Data,
Main
}
public class MySceneManager : MonoBehaviour
{
    public GameObject[] Scenes;
    public static MySceneManager Instance;
    private void Awake()
    {
        Instance = this;
    }
    public void OnSceneChange(int num)
    {
        for (int i = 0; i < Scenes.Length; i++)
        {
            Scenes[i].SetActive(false);
        }
        Scenes[num].SetActive(true);
    }
    public void OnExitButton()
    {
        Application.Quit();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            //重新讀取場景
            SceneManager.LoadScene(0);
            //Application.Quit();
        }
    }
}

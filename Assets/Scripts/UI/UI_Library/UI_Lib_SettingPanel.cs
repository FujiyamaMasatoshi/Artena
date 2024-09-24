using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UI_Lib_SettingPanel : MonoBehaviour
{
    [SerializeField] private GameObject settingPanel = null;


    //private void Start()
    //{
    //    InitPanel();
    //}

    public void InitPanel()
    {
        ClosePanel();
    }


    public void ClosePanel()
    {
        settingPanel.SetActive(false);
    }

    public void OpenPanel()
    {
        settingPanel.SetActive(true);
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}

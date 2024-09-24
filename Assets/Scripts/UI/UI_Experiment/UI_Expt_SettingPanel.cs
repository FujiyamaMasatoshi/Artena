using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// ExperimentSceneでの設定パネル
public class UI_Expt_SettingPanel : MonoBehaviour
{
    [SerializeField] GameObject settingPanel = null;

    private void Start()
    {
        InitPanel()
;    }

    public void InitPanel()
    {
        ClosePanel();
    }

    public void OpenPanel()
    {
        settingPanel.SetActive(true);
    }

    public void ClosePanel()
    {
        settingPanel.SetActive(false);
    }

    public void LoadMainScene()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void LoadLibraryScene()
    {
        SceneManager.LoadScene("Library");
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TitleEvent : MonoBehaviour
{
    [SerializeField] private GameObject confirmPanel = null;
    [SerializeField] private Button continueButton = null;

    [SerializeField] private GameObject clearDataPanel = null;
    [SerializeField] private GameObject settingPanel;
    [SerializeField] private GameObject creditPanel;
    private bool isOpenSettingPanel = false;

    private void Start()
    {
        confirmPanel.SetActive(false);
        clearDataPanel.SetActive(false);
        settingPanel.SetActive(false);
        isOpenSettingPanel = false;
        creditPanel.SetActive(false);

        // continueできるかチェックする
        CheckCanContinue();
    }


    private void CheckCanContinue()
    {
        PlayerDataManager.instance.LoadPlayerData();

        string playerName = PlayerPrefs.GetString("PlayerName");

        // playerNameが存在しない場合
        if (playerName == "")
        {
            continueButton.interactable = false;
        }
        else
        {
            continueButton.interactable = true;
        }
    }

    // settingボタンを押したら呼び出す
    public void OnClickedSettingIcon()
    {
        if (!isOpenSettingPanel)
        {
            settingPanel.SetActive(true);
            isOpenSettingPanel = true;
        }
        else
        {
            settingPanel.SetActive(false);
            isOpenSettingPanel = false;
        }
    }

    // DeleteButtonを押したら
    public void OpenClearDataPanel()
    {
        clearDataPanel.SetActive(true);
        settingPanel.SetActive(false);
        isOpenSettingPanel = false;
    }

    public void ClearPlayerData()
    {   
        PlayerDataManager.instance.DeletePlayerData();
        CheckCanContinue();

        // パネルを閉じる
        CloseClearDataPanel();
    }

    public void CloseClearDataPanel()
    {
        clearDataPanel.SetActive(false);
    }

    // Continueボタンを押したら呼び出す
    public void LoadPlayerData()
    {
        PlayerDataManager.instance.LoadPlayerData();

        // MainMenuシーンへ移動
        SceneManager.LoadScene("MainMenu");
    }

    // NewGameボタンを押したら呼び出す
    public void DisplayCanvas()
    {
        // ContinueButtonがinteractableがfalseだったら、即座にNewGameSceneに遷移
        if (continueButton.interactable == false)
        {
            GoToNewGameScene();
        }
        else
        {
            confirmPanel.SetActive(true);
        }
        
    }

    // キャンバスを非表示にする
    public void UndisplayCanvas()
    {
        confirmPanel.SetActive(false);
    }

    // NewGameシーンへ移動
    public void GoToNewGameScene()
    {
        SceneManager.LoadScene("NewGameScene");
    }

    // creditボタンを押したら
    public void OpenCreditPanel()
    {
        creditPanel.SetActive(true);
        settingPanel.SetActive(false);
        isOpenSettingPanel = false;
    }
    public void CloseCreditPanel()
    {
        creditPanel.SetActive(false);
    }
}

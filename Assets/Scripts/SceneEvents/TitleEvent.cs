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

    private void Start()
    {
        confirmPanel.SetActive(false);
        clearDataPanel.SetActive(false);

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
    public void OpenClearDataPanel()
    {
        clearDataPanel.SetActive(true);
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
}

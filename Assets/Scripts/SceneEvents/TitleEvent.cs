using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TitleEvent : MonoBehaviour
{
    [SerializeField] private GameObject newGameCanvas = null;
    [SerializeField] private Button continueButton = null;

    private void Start()
    {
        newGameCanvas.SetActive(false);

        // continueできるかチェックする
        CheckCanContinue();
    }


    private void CheckCanContinue()
    {
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
            newGameCanvas.SetActive(true);
        }
        
    }

    // キャンバスを非表示にする
    public void UndisplayCanvas()
    {
        newGameCanvas.SetActive(false);
    }

    // NewGameシーンへ移動
    public void GoToNewGameScene()
    {
        SceneManager.LoadScene("NewGameScene");
    }
}

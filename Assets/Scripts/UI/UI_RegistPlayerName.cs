using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class UI_RegistPlayerName : MonoBehaviour
{
    [SerializeField] private TMP_InputField inputField = null;
    [SerializeField] private GameObject registPlayerNameCanvas = null;
    [SerializeField] private TextMeshProUGUI popUpMessage = null;

    private void Start()
    {
        registPlayerNameCanvas.SetActive(true);
        popUpMessage.gameObject.SetActive(false);


    }

    // プレイヤー名を登録
    public void RegistPlayerName()
    {
        string playerName = inputField.text;
        if (playerName == "")
        {
            Debug.Log("プレイヤー名が空白です。もう一度入力してください");
            StartCoroutine(DisplayErrorMessage());
        }
        else
        {
            // プレイヤー名を設定
            PlayerDataManager.instance.SavePlayerName(playerName);

            // 初期fewShotの設定
            PlayerDataManager.instance.InitSkillLibrary();

            // 属性のランダムで設定
            PlayerDataManager.instance.InitAttribute();

            // プレイヤーデータをロード
            PlayerDataManager.instance.LoadPlayerData();


            StartCoroutine(DisplayPlayerName());

            
        }
        
    }

    private IEnumerator DisplayPlayerName()
    {
        string playerName = PlayerPrefs.GetString("PlayerName");
        string message = $"プレイヤー名を{playerName}と設定しました。";
        popUpMessage.text = message;
        popUpMessage.gameObject.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        popUpMessage.gameObject.SetActive(false);


        // 登録後、パネルをactive falseに設定
        registPlayerNameCanvas.SetActive(false);


        // MainMenuシーンへ遷移
        SceneManager.LoadScene("MainMenu");
    }

    private IEnumerator DisplayErrorMessage()
    {
        string message = "プレイヤー名が設定されていません。\nもう一度入力してください。";
        popUpMessage.text = message;
        popUpMessage.gameObject.SetActive(true);
        yield return new WaitForSeconds(1.0f);
        popUpMessage.gameObject.SetActive(false);

    }
}
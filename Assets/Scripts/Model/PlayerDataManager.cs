using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 個別のプレイヤーデータを管理する
public class PlayerDataManager : MonoBehaviour
{
    public static PlayerDataManager instance = null;
    public string playerName = "";

    // startの前に呼び出される
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

    }

    public void LoadPlayerData()
    {
        playerName = PlayerPrefs.GetString("PlayerName");

    }

    // これまでのデータを全て消去し新しくデータを構築する
    public void DeletePlayerData()
    {
        // 全てのデータを消去
        PlayerPrefs.DeleteAll();
    }


    // プレイヤー名を保存
    public void SavePlayerName(string playerName)
    {
        PlayerPrefs.SetString("PlayerName", playerName);
    }

}

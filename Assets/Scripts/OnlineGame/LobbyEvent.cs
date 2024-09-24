using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;
using Photon.Realtime;
using TMPro;

public class LobbyEvent : MonoBehaviourPunCallbacks
{
    [SerializeField, Header("CreateRoom")] private TMP_InputField inputField;
    [SerializeField, Header("CreateRoom")] private GameObject createRoomPanel;
    [SerializeField, Header("SelectRoom")] private GameObject selectRoomPanel;
    [SerializeField, Header("SelectRoom")] private TextMeshProUGUI[] roomListTexts;
    
    // 現在のroomの情報を保持
    public List<string> rooms = new List<string>();




    private void Start()
    {
        // マスターサーバーへ接続する
        PhotonNetwork.ConnectUsingSettings();

        //// Photon関係の初期化
        //InitLobby();
        // パネルの非表示
        createRoomPanel.SetActive(false);
        selectRoomPanel.SetActive(false);
        // roomの初期化
        rooms = new List<string>();
        rooms.Clear();


    }
    // == ロビー関連 ======================
    public override void OnConnectedToMaster()
    {
        InitLobby();
    }
    // Start時に呼び出す
    public void InitLobby()
    {
        // ロビーに参加する
        PhotonNetwork.JoinLobby();

        

        // roomListTextに表示
        DisplayCurrentRoom();
    }

    // ロビーに参加したら呼ばれる
    public override void OnJoinedLobby()
    {
        Debug.Log("ロビーに参加しました");
    }

    // ロビーから退出する操作
    public void LeaveLobby()
    {
        // ロビーから退出
        PhotonNetwork.LeaveLobby();
        // その後、自動的にOnLeftLobbyが呼ばれる
    }
    // ロビーから退出したら呼ばれる
    public override void OnLeftLobby()
    {
        Debug.Log("ロビーから退出しました");
        
        // MainMenuシーンへ戻る
        SceneManager.LoadScene("MainMenu");
    }

    // RoomInfoが更新されたら呼び出す
    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        // roomsも一緒に更新する
        rooms.Clear();

        foreach (var info in roomList)
        {
            if (!info.RemovedFromList)
            {
                Debug.Log($"ルーム更新: {info.Name}({info.PlayerCount}/{info.MaxPlayers})");

                // roomの名前を取得
                rooms.Add(info.Name);

                // roomListTextに表示
                DisplayCurrentRoom();

            }
            else
            {
                Debug.Log($"ルーム削除: {info.Name}");
            }
        }
    }

    public void CreateRoom()
    {
        if (inputField.text != "")
        {
            var roomOpt = new RoomOptions();
            // ルーム名
            string roomName = inputField.text;

            // 最大人数
            roomOpt.MaxPlayers = 2;


            // roomsに追加
            PhotonNetwork.CreateRoom(roomName, roomOpt);
        }
        else
        {
            Debug.Log("ルーム名を入力していません。");
        }
        
    }

    public override void OnCreatedRoom()
    {
        Debug.Log("ルームの作成に成功!!");
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        Debug.Log($"ルームの作成に失敗..{message}");
    }

    // =====================================

    // == CreatePanelとSelectPanelを表示 ====
    public void OpenCreateRoomPanel()
    {
        createRoomPanel.SetActive(true);
    }
    public void CloseCreateRoomPanel()
    {
        createRoomPanel.SetActive(false);
    }

    public void OpenSelectRoomPanel()
    {
        selectRoomPanel.SetActive(true);
    }
    public void CloseSelectRoomPanel()
    {
        selectRoomPanel.SetActive(false);
    }
    // =======================================

    // == Display Panel =====================
    public void DisplayCurrentRoom()
    {
        if (rooms.Count > 0)
        {
            for(int i=0; i< roomListTexts.Length; i++)
            {
                roomListTexts[i].text = rooms[i];

                if (i>rooms.Count)
                {
                    roomListTexts[i].gameObject.SetActive(false);
                }
            }


        }
        else
        {
            foreach(TextMeshProUGUI roomInfo in roomListTexts)
            {
                roomInfo.gameObject.SetActive(false);
            }
        }
    }


}

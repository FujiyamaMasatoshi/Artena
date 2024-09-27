using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UI_SetPlayerName : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI playerName;
    [SerializeField] private GameObject selectAttPanel;
    private bool isOnSelectAttPanel = false;
    

    // Start is called before the first frame update
    void Start()
    {
        SetPlayerName();
        CloseSelectAttPanel();
    }

    private void SetPlayerName()
    {
        PlayerDataManager.instance.LoadPlayerData();
        playerName.text = PlayerDataManager.instance.playerName;
    }


    // == button event =============
    public void OnClicedPlayerInfo()
    {
        if (isOnSelectAttPanel) CloseSelectAttPanel();
        else OpenSelectAttPanel();
    }



    public void OpenSelectAttPanel()
    {
        selectAttPanel.SetActive(true);
        isOnSelectAttPanel = true;
    }

    public void CloseSelectAttPanel()
    {
        selectAttPanel.SetActive(false);
        isOnSelectAttPanel = false;
    }

}

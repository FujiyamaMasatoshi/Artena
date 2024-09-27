using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_AttributeIcon : MonoBehaviour
{
    [SerializeField] private Image[] icons = null;
    [SerializeField] private Image icon = null;
    [SerializeField] private GameObject selectAttPanel;

    private void Start()
    {
        // attribute iconの初期設定
        PlayerDataManager.instance.LoadPlayerData();
        SetIcon();
    }

    private string selectedAttribute = "";


    public void SetIcon()
    {

        if (PlayerDataManager.instance.attribute == "cute")
        {
            icon.sprite = icons[0].sprite;
            selectAttPanel.SetActive(false);
            PlayerDataManager.instance.SaveAttribute();
        }
        else if (PlayerDataManager.instance.attribute == "cool")
        {
            icon.sprite = icons[1].sprite;
            selectAttPanel.SetActive(false);
            PlayerDataManager.instance.SaveAttribute();
        }
        else if (PlayerDataManager.instance.attribute == "unique")
        {
            icon.sprite = icons[2].sprite;
            selectAttPanel.SetActive(false);
            PlayerDataManager.instance.SaveAttribute();
        }
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UI_AttButtonManager : MonoBehaviour
{
    [SerializeField] private UI_CuteButton cuteButton;
    [SerializeField] private UI_CoolButton coolButton;
    [SerializeField] private UI_UniqueButton uniqueButton;
    [SerializeField] private TextMeshProUGUI detailText;
    [SerializeField] private GameObject decideButton;

    private void Start()
    {
        Init();
    }

    private void Init()
    {
        cuteButton.InitButton();
        coolButton.InitButton();
        uniqueButton.InitButton();
        detailText.text = "";
        detailText.gameObject.SetActive(false);
        decideButton.SetActive(false);

        PlayerDataManager.instance.LoadPlayerData();
    }

    public void OnCuteButton()
    {
        detailText.gameObject.SetActive(true);
        decideButton.SetActive(true);

        detailText.text = cuteButton.details;
        cuteButton.isSelect = true;
        PlayerDataManager.instance.attribute = "cute";

        // 他のボタンのisSelectをfalseにする
        coolButton.isSelect = false;
        uniqueButton.isSelect = false;
    }

    public void OnCoolButton()
    {
        detailText.gameObject.SetActive(true);
        decideButton.SetActive(true);

        detailText.text = coolButton.details;
        coolButton.isSelect = true;
        PlayerDataManager.instance.attribute = "cool";

        // 他のボタンのisSelectをfalseにする
        cuteButton.isSelect = false;
        uniqueButton.isSelect = false;
    }

    public void OnUniqueButton()
    {
        detailText.gameObject.SetActive(true);
        decideButton.SetActive(true);

        detailText.text = uniqueButton.details;
        uniqueButton.isSelect = true;
        PlayerDataManager.instance.attribute = "unique";

        // 他のボタンのisSelectをfalseにする
        cuteButton.isSelect = false;
        coolButton.isSelect = false;

    }



}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BattlerStatus : MonoBehaviour
{
    [SerializeField] private Battler battler = null; // battler instance
    [SerializeField] private Image hpBar = null; // hp bar image
    [SerializeField] private TextMeshProUGUI hpText = null; // int hp to str
    [SerializeField] private TextMeshProUGUI playerName = null;

    private void Update()
    {
        hpText.text = $"{battler.hp}/{battler.maxHp}";
        playerName.text = battler.playerName;

        // hp barの反映
        hpBar.fillAmount = (float)battler.hp / (float)battler.maxHp;
        
    }

}

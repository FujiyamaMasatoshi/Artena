using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UI_TB_BattleResult : MonoBehaviour
{
    private TextMeshProUGUI resultText = null;

    private void Start()
    {
        resultText = GetComponent<TextMeshProUGUI>();
    }
    
    private void Update()
    {
        bool isWin = TB_GameManager.instance.IsWin();

        if (isWin) resultText.text = "WIN!!";
        else resultText.text = "Lose...";
    }

}

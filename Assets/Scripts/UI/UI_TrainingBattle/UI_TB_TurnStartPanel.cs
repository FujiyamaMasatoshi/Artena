using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UI_TB_TurnStartPanel : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI turnText;

    public void SetTurn(int numTurn)
    {
        turnText.text = $"Turn\n{numTurn}";
    }
}

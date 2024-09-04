using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

// gameのターンのみを表示させる
public class UI_TB_GameTurn : MonoBehaviour
{

    [SerializeField] private Game game = null;
    [SerializeField] private TextMeshProUGUI text = null;
    

    // Update is called once per frame
    void Update()
    {
        text.text = game.turn.ToString();
    }
}

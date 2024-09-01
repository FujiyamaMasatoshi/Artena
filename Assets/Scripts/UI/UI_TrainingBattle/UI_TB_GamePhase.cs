using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


// ゲームフェーズを表示
public class UI_TB_GamePhase : MonoBehaviour
{

    [SerializeField] private Game game = null;
    [SerializeField] private TextMeshProUGUI text = null;

    // Update is called once per frame
    void Update()
    {
        text.text = game.currentPhase.ToString();
    }
}

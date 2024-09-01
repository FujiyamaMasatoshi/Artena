using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


// battlerの詳細を表示するためのclass
public class UI_TB_Battler : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI statusText = null;
    [SerializeField] private TextMeshProUGUI logText = null;

    [SerializeField] Battler battler = null;

    [SerializeField] private TrainingBattle battle = null;
    [SerializeField] private Game game = null;

    // logとして表示させたいテキストをセットするメソッド
    public void SetLogText(string message)
    {
        logText.text = message;
    }

    // プレイヤーのステータスを表示
    private void DisplayPlayerStatus()
    {
        statusText.text = $"{battler.playerName}\n";
        statusText.text += $"HP: {battler.hp}";
    }



    private void Start()
    {
        logText.text = ""; //Start時のみ空白でセットする
    }
    private void Update()
    {
        DisplayPlayerStatus(); // プレイヤーのステータスを更新
        if (game.currentPhase == Game.GamePhase.TurnStart)
        {
            SetLogText("");
        }
        else if(game.currentPhase == Game.GamePhase.Generate)
        {
            Skill generatedSkill = battle.GetGeneratedSkill_Item1();
            if (generatedSkill != null) SetLogText(game.SkillDetails(generatedSkill));
            
        }
    }
}

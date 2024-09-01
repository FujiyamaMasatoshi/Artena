using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UI_TB_CpuLog : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI statusText = null;
    [SerializeField] private TextMeshProUGUI logText = null;

    [SerializeField] CpuBattler cpu = null;

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
        statusText.text = $"{cpu.cpuName}\n";
        statusText.text += $"HP: {cpu.hp}";
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
        else if (game.currentPhase == Game.GamePhase.Generate)
        {
            Skill generatedSkill = battle.GetGeneratedSkill_Item2();
            if (generatedSkill != null) SetLogText(game.SkillDetails(generatedSkill));

        }
        else if (game.currentPhase == Game.GamePhase.Execute)
        {
            //
        }
        else if(game.currentPhase == Game.GamePhase.Result)
        {
            //
        }
        else if (game.currentPhase == Game.GamePhase.GameEnd)
        {
            //
        }

    }
}

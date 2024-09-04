using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


// battlerの詳細を表示するためのclass
public class UI_TB_Battler : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI logText = null;

    [SerializeField] Battler battler = null;

    [SerializeField] private Game game = null;

    // logとして表示させたいテキストをセットするメソッド
    public void SetLogText(string message)
    {
        logText.text = message;
    }

    


    private void Start()
    {
        logText.text = ""; //Start時のみ空白でセットする
    }
    private void Update()
    {
        if (game.currentPhase == Game.GamePhase.GameStart || game.currentPhase == Game.GamePhase.TurnStart)
        {
            SetLogText("");
        }
        else if(game.currentPhase == Game.GamePhase.Generate)
        {
            Skill generatedSkill = battler.GetGeneratedSkill();
            if (generatedSkill != null) SetLogText(game.SkillDetails(generatedSkill));
            
        }
    }
}

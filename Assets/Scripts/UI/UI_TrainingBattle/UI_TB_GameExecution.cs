using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


// ゲームで何が行われているかを表示する
public class UI_TB_GameExecution : MonoBehaviour
{
    [SerializeField] private TrainingBattle battle = null;
    [SerializeField] private TextMeshProUGUI text = null;

    // Update is called once per frame
    void Update()
    {
        if (battle.isGenerating)
        {
            text.text = "スキル生成中";
        }
        else if (battle.isGenerated)
        {
            text.text = "スキル生成完了";
        }
        else if(!battle.isGenerated && !battle.isGenerating)
        {
            text.text = "";
        }
    }
}

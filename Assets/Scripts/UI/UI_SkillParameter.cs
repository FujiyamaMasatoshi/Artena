using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UI_SkillParameter : MonoBehaviour
{
    [SerializeField] private Game game;
    [SerializeField] private GameObject displayParams = null;

    [SerializeField] private TextMeshProUGUI skillName = null;

    // パラメーターバー
    [SerializeField] private Image CuteBar = null; //filled && horizonalに設定すること
    [SerializeField] private Image CoolBar = null;
    [SerializeField] private Image UniqueBar = null;

    // パラメータテキスト
    [SerializeField] private TextMeshProUGUI cutePoint = null;
    [SerializeField] private TextMeshProUGUI coolPoint = null;
    [SerializeField] private TextMeshProUGUI uniquePoint = null;

    // 操作キャラクタならtrue, それ以外false
    [SerializeField] bool isMine; 

    // スキルを受け取り、スキルパラメータをUIに反映
    private void ReflectSkillParameter(Skill skill)
    {
        // スキル名
        skillName.text = skill.skillName;

        // スキルパラメータをfloatにキャスト
        float cute = (float)skill.parameters.cute;
        float cool = (float)skill.parameters.cool;
        float unique = (float)skill.parameters.unique;

        // 画像のfillAmountに反映
        CuteBar.fillAmount = cute / (cute + cool + unique);
        CoolBar.fillAmount = cool / (cute + cool + unique);
        UniqueBar.fillAmount = unique / (cute + cool + unique);

        // テキストに数値を反映
        cutePoint.text = skill.parameters.cute.ToString();
        coolPoint.text = skill.parameters.cool.ToString();
        uniquePoint.text = skill.parameters.unique.ToString();
    }

    private void ClearUI()
    {
        displayParams.SetActive(false);
    }

    private void Update()
    {
        // gameから生成されたスキルを取得
        // nullなら非表示
        // null以外なら、表示
        if (game.currentPhase == Game.GamePhase.Generate || game.currentPhase == Game.GamePhase.Execute || game.currentPhase == Game.GamePhase.Result)
        {
            
            Skill skill = null;
            if (isMine) skill = game.generatedSkillInGeneratePhase.Item1;
            else skill = game.generatedSkillInGeneratePhase.Item2;
            if (skill != null)
            {
                displayParams.SetActive(true);
                // 数値を反映
                ReflectSkillParameter(skill);
            }
        }
        else
        {
            ClearUI();
        }
        
    }

}

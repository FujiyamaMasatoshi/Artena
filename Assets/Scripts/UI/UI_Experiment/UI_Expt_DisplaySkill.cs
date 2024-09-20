using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UI_Expt_DisplaySkill : MonoBehaviour
{
    // 実験場イベント
    [SerializeField] private ExperimentEvent exptEvent = null;

    // スキル名
    [SerializeField] private TextMeshProUGUI skillName = null;
    // パラメーターバー
    [SerializeField] private Image CuteBar = null; //filled && horizonalに設定すること
    [SerializeField] private Image CoolBar = null;
    [SerializeField] private Image UniqueBar = null;

    // パラメータテキスト
    [SerializeField] private TextMeshProUGUI cutePoint = null;
    [SerializeField] private TextMeshProUGUI coolPoint = null;
    [SerializeField] private TextMeshProUGUI uniquePoint = null;

    private Skill displayedSkill = null;


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

    private void Update()
    {
        displayedSkill = exptEvent.GetGeneratedSkill();
        if (displayedSkill != null)
        {
            ReflectSkillParameter(displayedSkill);
        }
    }

}

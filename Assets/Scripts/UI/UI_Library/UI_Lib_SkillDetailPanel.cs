using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UI_Lib_SkillDetailPanel : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI skillName = null;

    // パラメーターバー
    [SerializeField] private Image CuteBar = null; //filled && horizonalに設定すること
    [SerializeField] private Image CoolBar = null;
    [SerializeField] private Image UniqueBar = null;

    // パラメータテキスト
    [SerializeField] private TextMeshProUGUI cutePoint = null;
    [SerializeField] private TextMeshProUGUI coolPoint = null;
    [SerializeField] private TextMeshProUGUI uniquePoint = null;

    //private void Start()
    //{
    //    Init();
    //}

    // 初期化メソッド
    public void Init()
    {
        gameObject.SetActive(false);
    }

    // スキルを受け取り、スキルパラメータをUIに反映
    public void ReflectSkillParameter(Skill skill)
    {
        if (skill.skillName != "")
        {
            // active trueにする
            gameObject.SetActive(true);

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
    }


    // 表示させているスキルをfewShotとしてセットする
    public void SetFewShotSkill()
    {
        // 何か表示させていたら
        if (skillName.text != "")
        {
            Skill skill = new Skill(skillName.text, new SkillParams(int.Parse(cutePoint.text), int.Parse(coolPoint.text), int.Parse(uniquePoint.text)));

            // PlayerDataManagerのsetFewShotメソッドを呼び出す
            PlayerDataManager.instance.SetFewShotSkill(skill);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UI_Lib_SkillPanel : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI skillName = null;
    [SerializeField] private TextMeshProUGUI attPoint = null;
    [SerializeField] private Image attIcon = null;
    [SerializeField] private Image[] attIcons;

    [SerializeField] private UI_Lib_SkillDetailPanel skillDetailPanel = null;
    [SerializeField] private GameObject isSelectedPanel = null; //選択されている時選択中と表示させるパネル
    [SerializeField] private UI_Lib_SelectSkillPanel selectSkillPanel = null;
 
    // 表示させているスキル
    [SerializeField] private Skill displayedSkill = null;

    public bool isSelect = false;

    // 初期化された時、skillNameとattPointを割り当てる
    public void Init(Skill skill)
    {
        displayedSkill = skill;

        SetSkillPanel(skill);
        isSelect = false;
        isSelectedPanel.SetActive(false);
    }


    public void SetSkillPanel(Skill skill)
    {
        //スキル名を設定
        SetSkillName(skill);

        // 属性ポイントのセット
        SetAttribute(skill);
    }


    public void SetActiveIsSelectedPanel(bool b)
    {
        isSelectedPanel.SetActive(b);
        isSelect = false;
    }

    // スキルパネルをクリックした時呼び出す
    public void OnSkillPanelClicked()
    {
        // 全てのスキルパネルの選択中パネルを非表示にする
        selectSkillPanel.OnClicked();

        // 選択情報を更新
        // 選択されていた時
        if (isSelect)
        {
            isSelect = false;
            isSelectedPanel.SetActive(false);

        }
        else if (!isSelect)
        {
            isSelect = true;
            isSelectedPanel.SetActive(true);
        }

        // detailPanelに情報を表示
        skillDetailPanel.ReflectSkillParameter(displayedSkill);
    }

    private void SetSkillName(Skill skill)
    {
        skillName.text = skill.skillName;
    }

    private void SetAttribute(Skill skill)
    {
        // att pointから
        string att = skill.Attribute();
        if (att == "cute")
        {
            attPoint.text = skill.parameters.cute.ToString();
            attIcon.sprite = attIcons[0].sprite;
        }
        else if (att == "cool")
        {
            attPoint.text = skill.parameters.cool.ToString();
            attIcon.sprite = attIcons[1].sprite;
        }
        else if (att == "unique")
        {
            attPoint.text = skill.parameters.unique.ToString();
            attIcon.sprite = attIcons[2].sprite;
        }
    }

}

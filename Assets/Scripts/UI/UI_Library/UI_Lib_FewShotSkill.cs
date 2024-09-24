using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


// fewShotスキルを管理
public class UI_Lib_FewShotSkill : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI skillPointText = null;
    [SerializeField] private TextMeshProUGUI skillName = null;
    [SerializeField] private UI_Lib_SkillDetailPanel skillDetailPanel = null;
    [SerializeField] private UI_Lib_SelectSkillPanel selectSkillPanel = null;
    [SerializeField] private int fewShotIndex = 0; // cute担当なら0, cool:1, unique:2

    private Skill displayedSkill = null;
    private string beforeSkillName = "";

    //private void Start()
    //{
    //    Init();
    //}

    public void Init()
    {
        //PlayerDataManager.instance.LoadSkillLibrary();
        displayedSkill = PlayerDataManager.instance.skillLibrary.fewShot[fewShotIndex];
        beforeSkillName = displayedSkill.skillName;
        DisplaySkill();
    }

    private void Update()
    {
        displayedSkill = PlayerDataManager.instance.skillLibrary.fewShot[fewShotIndex];
        if (displayedSkill.skillName != beforeSkillName)
        {
            DisplaySkill();
            beforeSkillName = displayedSkill.skillName;
        }
    }


    private void ReflectDetailPanel()
    {
        skillDetailPanel.ReflectSkillParameter(displayedSkill);
    }
    

    private void DisplaySkill()
    {
        int skillPoint = 0;
        if (displayedSkill.Attribute() == "cute")
        {
            skillPoint = displayedSkill.parameters.cute;
        }
        else if(displayedSkill.Attribute() == "cool")
        {
            skillPoint = displayedSkill.parameters.cool;
        }
        else if(displayedSkill.Attribute() == "unique")
        {
            skillPoint = displayedSkill.parameters.unique;
        }
        skillPointText.text = skillPoint.ToString();
        skillName.text = displayedSkill.skillName;

    }


    // ボタンクリックされたら、skillDetailPanelに表示させる
    public void OnClicked()
    {
        // skillPanelの選択状態を解除
        selectSkillPanel.OnClicked();

        // detailPanelに反映
        ReflectDetailPanel();
        // UIに表示
        DisplaySkill();
    }
}

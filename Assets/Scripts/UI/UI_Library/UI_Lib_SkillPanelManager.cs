using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Lib_SkillPanelManager : MonoBehaviour
{
    [SerializeField] private UI_Lib_SkillPanel[] skillPanels;
    [SerializeField] private SkillPanelDataManager skillPanelDataManager = null;

    private int beforePageIndex = 0;

    //private void Start()
    //{
    //    InitPanel(); //全てのskillPanel.Init()を呼び出し初期化
        

    //}

    private void Update()
    {
        int currentPageIndex = skillPanelDataManager.currentPageIndex;
        if (currentPageIndex != beforePageIndex)
        {
            Debug.Log("call SetSkillPanels()");
            SetSkillPanels();
            beforePageIndex = currentPageIndex;
        }

    }

    public void InitPanel()
    {
        // データマネージャーの初期化
        skillPanelDataManager.InitSkillPanelData();
        SetSkillPanels();
        // データマネージャーから現在のページインデックスをセット
        beforePageIndex = skillPanelDataManager.currentPageIndex;
    }

    private void SetSkillPanels()
    {
        // 全てのパネルをactive trueにする
        foreach(UI_Lib_SkillPanel sp in skillPanels)
        {
            sp.gameObject.SetActive(true);
        }

        // 表示させる
        List<Skill> displayedSkill = skillPanelDataManager.GetDisplayedSkills();
        int iter = displayedSkill.Count;
        if (iter > 0)
        {
            for (int i = 0; i < iter; i++)
            {
                UI_Lib_SkillPanel skillPanel = skillPanels[i];
                skillPanel.Init(displayedSkill[i]);
                //skillPanel.SetSkillPanel(displayedSkill[i]);
            }

            // skillPanelsの数より少ない場合
            // 画面から消す
            if (iter < skillPanels.Length)
            {
                for (int i=iter; i<skillPanels.Length; i++)
                {
                    skillPanels[i].gameObject.SetActive(false);
                }
            }
        }
        
    }

}

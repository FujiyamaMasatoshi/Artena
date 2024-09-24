using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Lib_PanelManager : MonoBehaviour
{
    [SerializeField] private UI_Lib_SkillDetailPanel skillDetailPanel;
    [SerializeField] private UI_Lib_SettingPanel settingPanel;
    [SerializeField] private UI_Lib_SkillPanelManager skillPanelManager;
    [SerializeField] private UI_Lib_FewShotSkill[] fewShotSkills;

    [SerializeField] private GameObject uiCanvas;

    private void Start()
    {
        // 初めはuiを見せない
        uiCanvas.SetActive(false);
    }

    // 全てのUIの初期化を行う
    public void InitializeAllUI()
    {
        // uiを表示
        uiCanvas.SetActive(true);

        skillDetailPanel.Init();
        settingPanel.InitPanel();
        skillPanelManager.InitPanel();
        foreach (UI_Lib_FewShotSkill fewShot in fewShotSkills)
        {
            fewShot.Init();
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Lib_SelectSkillPanel : MonoBehaviour
{
    [SerializeField] private UI_Lib_SkillPanel[] skillPanels;

    // クリックした時呼び出す
    public void OnClicked()
    {
        //全てのパネルのisSelectPanelを非表示にする
        // その後、各SkillPanelで自身のisSelectedPanelを表示させる
        foreach(UI_Lib_SkillPanel skillPanel in skillPanels)
        {
            skillPanel.SetActiveIsSelectedPanel(false);
        }
    }
}

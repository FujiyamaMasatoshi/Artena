using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UI_TB_CutInPanel : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI skillNameText;
    [SerializeField] private TextMeshProUGUI skillDetailsText;
    [SerializeField] private TextMeshProUGUI skillTypeText;
    [SerializeField] private Image cutInImage;

    [SerializeField] private Battler battler;

    public void SetCutInPanel(SkillType skillType)
    {
        Skill skill = battler.GetGeneratedSkill();
        skillNameText.text = skill.skillName;
        skillDetailsText.text = skill.skillDetails;

        // skillTypeによって表示する項目を変更する
        if (skillType == SkillType.Critical)
        {
            skillTypeText.text = "Critical !!\nx 1.5";

            // orange 
            cutInImage.color = new Color(243f / 255f, 171f / 255f, 95/255f, 255f/255f);
        }
        else if (skillType == SkillType.WeakPoint)
        {
            skillTypeText.text = "WeakPoint !!\nx 1.3";

            // orange 
            cutInImage.color = new Color(243f / 255f, 95f / 255f, 164f / 255f, 255f / 255f);
        }
    }

    public void ClearCutInPanel()
    {
        skillNameText.text = "";
        skillDetailsText.text = "";

    }
}

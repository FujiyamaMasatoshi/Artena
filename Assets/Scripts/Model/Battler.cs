using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Battler : MonoBehaviour
{
    [SerializeField] private TMP_InputField inputField = null;
    [SerializeField] private SkillGenerator skillGenerator = null;


    // status
    public string playerName = "Max";
    public int hp = 1000;


    // Battler側でスキル生成メソッドを呼び出す
    public void GenerateSkill()
    {
        if ((inputField.text != "" || inputField.text != null) && !skillGenerator.isGenerating)
        {
            skillGenerator.SetSkillName(inputField.text);
            Debug.Log("skill name: "+skillGenerator.GetSkillName());

            //await Task.Run(() => skillGenerator.GenerateSkill());
            skillGenerator.GenerateSkill();
        }
    }

    // 生成したスキルをクリア
    public void ClearGeneratedSkill()
    {
        skillGenerator.ClearGeneratedSkill();
    }

    public Skill GetGeneratedSkill()
    {
        if (skillGenerator.GetGeneratedSkill() != null)
        {
            return skillGenerator.GetGeneratedSkill();
        }
        else
        {
            return null;
        }
    }

}

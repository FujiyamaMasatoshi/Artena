using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class CpuBattler : MonoBehaviour
{
    [SerializeField] private SkillGenerator skillGenerator = null;

    public string cpuName = "試練の番人";
    public int hp = 1000;


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

    // 生成したスキルをクリア
    public void ClearGeneratedSkill()
    {
        skillGenerator.ClearGeneratedSkill();
    }

    // ボタンクリックしたら呼び出す
    public void GenerateRandomSkill()
    {
        if (!skillGenerator.isGenerating)
        {
            skillGenerator.GenerateRandomSkill();
        }
    }

}

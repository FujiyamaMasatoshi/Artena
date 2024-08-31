using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// スキルパラメータ
[System.Serializable]
public class SkillParams
{
    public int cute;
    public int cool;
    public int unique;
}

// スキルクラス
[System.Serializable]
public class Skill
{
    public string skillName;
    public SkillParams parameters;
    

    public Skill(string skillName, SkillParams parameters)
    {
        this.skillName = skillName;
        this.parameters = parameters;
    }


    // Skillの属性を返す
    // 優先順位: cute > unique > cool
    public string Attribute()
    {
        if (parameters.cute >= parameters.cool && parameters.cute >= parameters.unique) return "cute";
        else if (parameters.unique >= parameters.cool && parameters.unique >= parameters.cute) return "unique";
        else return "cool";
    }

}
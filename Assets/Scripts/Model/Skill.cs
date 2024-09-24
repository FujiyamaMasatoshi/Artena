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

    public SkillParams(int cute, int cool, int unique)
    {
        this.cute = cute;
        this.cool = cool;
        this.unique = unique;
    }

    // 各種パラメータを合計値が100となるように正規化
    // 合計値がnSumとなるように正規化を行う
    public void NormalizeParams(float nSum=100f)
    {
        float sum = (float)(this.cute + this.cool + this.unique);

        this.cute = (int)((float)this.cute / sum * nSum);
        this.cool = (int)((float)this.cool / sum * nSum);
        this.unique = (int)((float)this.unique / sum * nSum);
    }
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
        parameters.NormalizeParams();
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

// スキルライブラリ
[System.Serializable]
public class SkillLibrary
{
    public List<Skill> library;
    public Skill[] fewShot;

    public SkillLibrary()
    {
        this.library = new List<Skill>();
        this.fewShot = new Skill[3];
    }



}
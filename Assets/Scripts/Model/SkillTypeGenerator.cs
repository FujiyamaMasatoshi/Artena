using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 役割: Skillを受け取り、そのスキルに対してタイプをつける
// 一定確率で、会心
// targetの属性から、弱点かどうかを判定
public enum SkillType
{
    Normal, //通常 -> この時はカットインをしない
    Critical, //会心発生 -> カットインを行う
    WeakPoint, // 弱点 -> カットインを行う
}

public class SkillTypeGenerator
{
    public SkillTypeGenerator(){ }

    public SkillType ComputeSkillType(Skill skill, Battler target, float criticRate, float criticDamage)
    {
        // 会心チェック
        int rand = Random.Range(0, 100);
        Debug.Log("rand: " + rand);
        if (rand < criticRate)
        {
            return SkillType.Critical;
        }

        // 弱点チェック
        if (IsWeakPoint(skill, target))
        {
            return SkillType.WeakPoint;
        }
        return SkillType.Normal;
    }

    // skillの属性が、battlerの弱点かどうかをチェック
    private bool IsWeakPoint(Skill skill, Battler battler)
    {
        string skillAtt = skill.Attribute();
        string battlerAtt = battler.attribute;

        if (skillAtt=="cute" && battlerAtt=="unique")
        {
            return true;
        }
        else if(skillAtt=="cool" && battlerAtt=="cute")
        {
            return true;
        }
        else if(skillAtt=="unique" && battlerAtt=="cool")
        {
            return true;
        }
        return false;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Battler : MonoBehaviour
{

    [SerializeField] private TMP_InputField inputField = null;
    [SerializeField] private SkillGenerator skillGenerator = null;
    [SerializeField] private UI_EffectGenerator effectGenerator = null;

    // status
    public string playerName = "Max";
    public int maxHp = 1000;
    public int hp = 1000;

    // スキル生成中かどうかを判断するflag
    public bool isGenerating = false; // スキル生成中
    public bool isExecute = false; // 実行開始


    public void InitBattler(int initHp)
    {
        maxHp = initHp;
        hp = maxHp;
        isGenerating = false;
        isExecute = false;

        // ここでLLMをロードする
        skillGenerator.InitSkillGenerator();
    }

    // Battler側でスキル生成メソッドを呼び出す
    public void GenerateSkill()
    {
        if ((inputField.text != "" || inputField != null) && !skillGenerator.isGenerating)
        {
            skillGenerator.SetSkillName(inputField.text);
            Debug.Log("skill name: " + skillGenerator.GetSkillName());

            //await Task.Run(() => skillGenerator.GenerateSkill());
            skillGenerator.GenerateSkill(); //スキル生成開始
            effectGenerator.InstantiateEffects(transform.position); //スキル生成エフェクト生成
        }
    }

    // ボタンクリックしたら呼び出す
    public void GenerateRandomSkill()
    {
        if (!skillGenerator.isGenerating)
        {
            skillGenerator.GenerateRandomSkill(); //スキル生成
            effectGenerator.InstantiateEffects(transform.position); //スキル生成エフェクト生成
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

    public void StartExecute(Transform target)
    {
        StartCoroutine(effectGenerator.EffectObjectWork(target));

    }

    // エフェクト中かどうか
    public bool IsEffecting()
    {
        return effectGenerator.isEffecting;
    }


}


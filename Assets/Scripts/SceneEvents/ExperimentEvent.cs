using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


// このクラスでさせること
// * スキル生成(スキル名入力 and ランダム)を行いgeneratedSkillにセット
// * 
public class ExperimentEvent : MonoBehaviour
{
    // スキル名取得のみに使用
    [SerializeField] private TMP_InputField inputField = null;

    [SerializeField] private SkillGenerator skillGenerator = null;
    [SerializeField] private UI_EffectGenerator effectGenerator = null;
    [SerializeField] private Transform effectInstTransform = null;

    private Skill generatedSkill = null; //生成されたスキル

    private void Start()
    {
        skillGenerator.InitSkillGenerator();
    }

    public Skill GetGeneratedSkill()
    {
        return generatedSkill;
    }

    // ボタンクリックで呼び出す
    public void StartGenerateSkill()
    {
        StartCoroutine(GenerateSkill());
    }

    // skillGeneratorからスキルを生成させ、generatedSkillにセット
    private IEnumerator GenerateSkill()
    {
        generatedSkill = null;
        if ((inputField.text != "" || inputField != null) && !skillGenerator.isGenerating)
        {
            skillGenerator.SetSkillName(inputField.text);
            Debug.Log("skill name: " + skillGenerator.GetSkillName());

            skillGenerator.GenerateSkill(); //スキル生成開始
            Vector3 instPos = effectInstTransform.position - 10 * new Vector3(0f, 1f, 0f);
            effectGenerator.InstantiateEffects(instPos);
        }


        yield return new WaitUntil(() => !skillGenerator.isGenerating); //推論終了まで待つ

        Debug.Log("生成完了");
        generatedSkill = skillGenerator.GetGeneratedSkill();

        if (generatedSkill == null)
        {
            GenerateSkill();
        }

    }




}

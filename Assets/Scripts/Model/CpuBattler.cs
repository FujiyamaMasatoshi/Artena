using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class CpuBattler : MonoBehaviour
{
    [SerializeField] private SkillGenerator skillGenerator = null;


    // test
    [SerializeField] private TextMeshProUGUI testText = null;


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

    // ボタンクリックしたら呼び出す
    public void GenerateRandomSkill()
    {
        if (!skillGenerator.isGenerating)
        {
            skillGenerator.GenerateRandomSkill();
        }
    }

    // test
    private void TestDisplay()
    {
        if (skillGenerator.GetGeneratedSkill() != null)
        {
            // test
            testText.text = $"skillName: {skillGenerator.GetGeneratedSkill().skillName}\n";
            testText.text += $"cute: {skillGenerator.GetGeneratedSkill().parameters.cute}\n";
            testText.text += $"cool: {skillGenerator.GetGeneratedSkill().parameters.cool}\n";
            testText.text += $"unique: {skillGenerator.GetGeneratedSkill().parameters.unique}\n";
            testText.text += $"att: {skillGenerator.GetGeneratedSkill().Attribute()}\n";
        }
    }
    private void Update()
    {
        TestDisplay();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Battler : MonoBehaviour
{
    [SerializeField] private TMP_InputField inputField = null;
    [SerializeField] private SkillGenerator skillGenerator = null;


    // test
    [SerializeField] private TextMeshProUGUI testText = null;



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

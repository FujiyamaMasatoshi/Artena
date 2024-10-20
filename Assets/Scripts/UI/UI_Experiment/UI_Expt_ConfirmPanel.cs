using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UI_Expt_ConfirmPanel : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI confirmText = null;
    [SerializeField] private TextMeshProUGUI beforeSkillText = null; //表示領域
    [SerializeField] private TextMeshProUGUI afterSkillText = null; //表示領域

    public void InitPanel()
    {
        gameObject.SetActive(false);
    }


    // ExperimentEventのCheckSaveSkill()で呼び出す
    public void PopUpMessage(Skill beforeSkill, Skill afterSkill)
    {
        // メッセージを表示
        // スキル名
        string skillName = beforeSkill.skillName;
        // メッセージの内容
        string message = $"「{skillName}」 はすでに生成されています。保存するとスキルパラメーターが上書きされてしまいます。\n本当に保存しますか?";

        confirmText.text = message;

        // beforeSkillTextにセット
        beforeSkillText.text = $"Before\n<{skillName}>\ncute : {beforeSkill.parameters.cute}\ncool : {beforeSkill.parameters.cool} \n unique : {beforeSkill.parameters.unique}";

        // afterSkillTextにセット
        afterSkillText.text = $"After\n<{skillName}>\ncute : {afterSkill.parameters.cute}\ncool : {afterSkill.parameters.cool} \n unique : {afterSkill.parameters.unique}";
    }
}

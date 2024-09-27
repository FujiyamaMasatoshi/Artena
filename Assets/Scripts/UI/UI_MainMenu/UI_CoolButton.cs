using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UI_CoolButton : MonoBehaviour
{
    public bool isSelect = false;
    public string details = "Cool:\n属性Coolは属性Cuteの敵には有効だが、\n属性Uniqueな敵には効果が発揮しにくい";

    public void InitButton()
    {
        isSelect = false;
        details = "Cool:\n属性Cuteなスキルに対しては耐性があるが、\n属性Uniqueなスキルには大ダメージを受けてしまう";
    }
}

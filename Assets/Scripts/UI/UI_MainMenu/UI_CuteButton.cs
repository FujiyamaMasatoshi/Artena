using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UI_CuteButton : MonoBehaviour
{
    public bool isSelect = false;
    public string details = "Cute:\n属性Cuteは属性Uniqueの敵には有効だが、\n属性Coolな敵には効果が発揮しにくい";

    public void InitButton()
    {
        isSelect = false;
        details = "Cute:\n属性Uniqueなスキルに対しては耐性があるが、\n属性Coolなスキルには大ダメージを受けてしまう";
    }
}

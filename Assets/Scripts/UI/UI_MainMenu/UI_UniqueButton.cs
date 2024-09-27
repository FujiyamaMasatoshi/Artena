using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UI_UniqueButton : MonoBehaviour
{
    public bool isSelect = false;
    public string details = "Unique:\n属性Uniqueは属性Coolの敵には有効だが、\n属性Cuteな敵には効果が発揮しにくい";

    public void InitButton()
    {
        isSelect = false;
        details = "Unique:\n属性Coolなスキルに対しては耐性があるが、\n属性Cuteなスキルには大ダメージを受けてしまう";
    }

}

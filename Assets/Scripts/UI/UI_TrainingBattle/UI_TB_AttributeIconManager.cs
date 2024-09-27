using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_TB_AttributeIcon : MonoBehaviour
{
    [SerializeField] private Image[] icons = null;
    [SerializeField] private Image icon = null;

    public void SetIcon()
    {
        if (PlayerDataManager.instance.attribute == "cute")
        {
            icon.sprite = icons[0].sprite;
            PlayerDataManager.instance.SaveAttribute();
        }
        else if (PlayerDataManager.instance.attribute == "cool")
        {
            icon.sprite = icons[1].sprite;
            PlayerDataManager.instance.SaveAttribute();
        }
        else if (PlayerDataManager.instance.attribute == "unique")
        {
            icon.sprite = icons[2].sprite;
            PlayerDataManager.instance.SaveAttribute();
        }
    }

    public void SetIconForCPU(Battler cpu)
    {
        if (cpu.attribute == "cute")
        {
            icon.sprite = icons[0].sprite;
        }
        else if (cpu.attribute == "cool")
        {
            icon.sprite = icons[1].sprite;
        }
        else if (cpu.attribute == "unique")
        {
            icon.sprite = icons[2].sprite;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UI_DamageEffect : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI damageText = null;

    public void SetDamage(int damage)
    {
        damageText.text = damage.ToString();
    }
    
}

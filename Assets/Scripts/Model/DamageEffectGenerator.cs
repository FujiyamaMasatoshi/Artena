using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// ダメージエフェクト生成 (battler, cpu)のダメージエフェクトを管理
public class DamageEffectGenerator : MonoBehaviour
{
    [SerializeField] private UI_DamageEffect damageEffect = null;
    [SerializeField] private Transform generatedPos = null;
    [SerializeField] private float maxScale = 3.0f;
    [SerializeField] private float scaleUpRate = 1.2f;
    private void Start()
    {
        damageEffect.gameObject.SetActive(false);
    }


    // damageを受け取り、それを表示させる
    public void DisplayDamage(int damage)
    {
        Debug.Log("damage: " + damage);
        // エフェクト表示
        damageEffect.gameObject.SetActive(true);
        
        // ダメージを設定
        damageEffect.SetDamage(damage);
    }


    public void ClearDamageEffect()
    {
        damageEffect.gameObject.SetActive(false);
    }


}

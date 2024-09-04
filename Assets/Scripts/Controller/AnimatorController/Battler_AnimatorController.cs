using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Battler_AnimatorController : MonoBehaviour
{
    [SerializeField] Battler battler = null; // battler
    [SerializeField] Animator anim = null; // アニメーター


    private void Update()
    {
        // スキル生成中の時
        if (battler.isGenerating)
        {
            anim.SetBool("isGenerating", true);
        }
        if (!battler.isGenerating)
        {
            anim.SetBool("isGenerating", false);
        }
        if (battler.isExecute)
        {
            anim.SetBool("isExecute", true);
        }
        if (!battler.isExecute)
        {
            anim.SetBool("isExecute", false);
        }
    }
}

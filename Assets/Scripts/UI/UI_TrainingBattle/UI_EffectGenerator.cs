using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_EffectGenerator : MonoBehaviour
{
    [SerializeField] private GameObject effect = null;
    [SerializeField] private Vector3 abovePos = new Vector3(0f, 10f, 0f);
    private Vector3 defaultPos;
    private bool isInstantiate = false; //生成したかどうか
    private GameObject instEf = null; //生成したオブジェクト

    // 生成したエフェクトを動かすためのパラメーター
    [SerializeField] private float efSpeed = 5f;
    [SerializeField] private Vector3 maxScale = new Vector3(5f, 5f, 5f);

    
    // エフェクト中かどうか
    public bool isEffecting = false;

    
    // スキル生成中にスキルを生成している風のエフェクトを呼び出す
    public void InstantiateEffects(Vector3 instPos)
    {
        // 生成される時初めてdefaultPosが設定される
        defaultPos = instPos + abovePos;

        if (!isInstantiate)
        {
            instEf = Instantiate(effect, instPos+abovePos, Quaternion.identity);
            
            isInstantiate = true;
        }
    }

    // 生成したeffectオブジェクトを取得
    public GameObject GetInstEffect()
    {
        return instEf;
    }

    // Executeで生成したエフェクトを動かすスクリプト
    public IEnumerator EffectObjectWork(Transform target)
    {
        if (instEf != null)
        {
            isEffecting = true;

            var distance = Vector3.Distance(instEf.transform.position, target.position);
            var expectTime = distance / efSpeed;

            float timer = 0.0f;

            // effectの向きをtargetと向かい合う様に設定する
            Vector3 targetDirection = target.position - instEf.transform.position;
            instEf.transform.rotation = Quaternion.LookRotation(-targetDirection);

            // その向きに沿って移動させる
            while (timer < expectTime)
            {
                timer += Time.deltaTime;

                // 線形保管を使用して位置を更新
                instEf.transform.position = Vector3.Lerp(defaultPos, target.position, timer / expectTime);
                yield return null;
                instEf.transform.localScale = Vector3.Lerp(new Vector3(1f, 1f, 1f), maxScale, timer / expectTime);
            }
        }

        // 移動したらDestroyEffectsを呼び出す
        float scale = 5.0f;
        float effectingTime = 2.0f;
        StartCoroutine(DestroyEffects(scale, effectingTime));

        isEffecting = false;
    }

    public IEnumerator DestroyEffects(float scale, float effectingTime)
    {
        if (instEf != null && isInstantiate)
        {
            float timer = 0.0f;

            while (timer < effectingTime)
            {
                timer += Time.deltaTime;

                // 0.2秒で3倍にして、0.8秒で0.1倍にする
                Vector3 defaultScale = instEf.transform.localScale;
                Vector3 minScale = new Vector3(0.1f, 0.1f, 0.1f);
                Vector3 maxScale = new Vector3(scale, scale, scale);
                if (timer < effectingTime*0.2f)
                {
                    instEf.transform.localScale = Vector3.Lerp(defaultScale, maxScale, timer / 0.2f);
                }
                else
                {
                    instEf.transform.localScale = Vector3.Lerp(maxScale, minScale, (timer - 0.2f) / 0.8f);
                }
                yield return null;

            }
            Destroy(instEf);
            isInstantiate = false;
        }

    }

}

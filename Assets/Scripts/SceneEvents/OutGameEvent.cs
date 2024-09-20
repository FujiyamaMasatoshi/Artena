using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OutGameEvent : MonoBehaviour
{
    [SerializeField] GameObject mainCamera = null;
    [SerializeField] Transform defaultCameraPos = null;
    [SerializeField] Transform outGameCameraPos = null;
    [SerializeField] float cameraSpeed = 5f;

    // 生成させるエフェクト
    [SerializeField] List<GameObject> effectsForWin = new List<GameObject>();
    [SerializeField] List<GameObject> effectsForLose = new List<GameObject>();

    // エフェクトをinstantiateするtransform
    [SerializeField] List<Transform> instEffectsPosForWin = new List<Transform>();
    [SerializeField] List<Transform> instEffectsPosForLose = new List<Transform>();

    // 生成したエフェクトを管理するリスト
    private List<GameObject> instEffects = new List<GameObject>();

    [SerializeField] private TrainingBattle battle = null;

    // 1. 勝った用のエフェクトを表示
    // 2. カメラをbattlerを移す様に移動する
    // 2. battlerのアニメーションを動かす
    // 3. もう一度 or Mainシーンにも取らせるかを選択させる




    public void StartEvent()
    {
        // イベントの内容を記述
        InstantiateEffects();
        StartCoroutine(MoveCameraOutGamePos());
    }

    public void InstantiateEffects()
    {
        // 勝ったら
        if (TB_GameManager.instance.IsWin())
        {
            //
            for (int i=0; i<instEffectsPosForWin.Count; i++)
            {
                GameObject efObj = Instantiate(effectsForWin[i], instEffectsPosForWin[i].position, Quaternion.identity);

                instEffects.Add(efObj); //生成したオブジェクトを管理するリストに追加
            }
        }
        // 負けたら
        else
        {
            //
            for (int i = 0; i < instEffectsPosForWin.Count; i++)
            {
                GameObject efObj = Instantiate(effectsForLose[i], instEffectsPosForLose[i].position, Quaternion.identity);

                instEffects.Add(efObj); //生成したオブジェクトを管理するリストに追加
            }
        }
    }

    // OutGameで指定した一にカメラを移動させる
    public IEnumerator MoveCameraOutGamePos()
    {
        var distance = Vector3.Distance(defaultCameraPos.position, outGameCameraPos.position);
        var expectTime = distance / cameraSpeed;

        float timer = 0f;

        while (timer < expectTime)
        {
            timer += Time.deltaTime;

            // 線形保管を使用して位置を更新
            mainCamera.transform.position = Vector3.Lerp(defaultCameraPos.position, outGameCameraPos.position, timer / expectTime);
            mainCamera.transform.rotation = Quaternion.Lerp(defaultCameraPos.rotation, outGameCameraPos.rotation, timer / expectTime);
            mainCamera.transform.localScale = Vector3.Lerp(defaultCameraPos.localScale, outGameCameraPos.localScale, timer / expectTime);

            yield return null;
        }
    }


    // buttonクリックで呼び出す
    public void RetryGame()
    {
        // 生成したエフェクトオブジェクトをDestroyする
        foreach (GameObject ef in instEffects)
        {
            Destroy(ef);
        }
        instEffects.Clear();

        // ゲームの際呼び出し
        battle.NewGame();
    }


    public void ExitTraining()
    {
        // メインシーンに戻る
        Debug.Log("back to main scene");
        SceneManager.LoadScene("MainMenu");
    }



}

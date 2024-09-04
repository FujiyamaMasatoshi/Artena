using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Main_MoveToTrainingEvent : MonoBehaviour
{
    [SerializeField] private GameObject moveToTrainingCanvas = null;
    private string playerTag = "Player";

    private void Start()
    {
        // 最初は消す
        moveToTrainingCanvas.SetActive(false);
    }


    public void MoveToTrainingBattle()
    {
        SceneManager.LoadScene("Training");
    }

    public void ExitEvent()
    {
        // イベントフラグを下ろす
        MainSceneManager.instance.isEventDoing = false;

        // canvasを消す
        moveToTrainingCanvas.SetActive(false);
    }


    private void OnTriggerEnter(Collider other)
    {
        // canvasをactive trueにして、シーン移動できる状態にする
        // playerは動かない様に設定する
        if (other.CompareTag(playerTag))
        {
            // canvasを立ち上げる
            moveToTrainingCanvas.SetActive(true);

            // イベント中フラグを立てる
            MainSceneManager.instance.isEventDoing = true;

            
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(playerTag))
        {
            MainSceneManager.instance.isEventDoing = false;
        }
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// メインメニューでモードを選択したら、それに応じた演出の実行を行う
public class MainMenuEvent : MonoBehaviour
{
    [SerializeField] private GameObject menuCanvas = null; // ボタンクリックした時、表示非表示のみを管理する
    [SerializeField] private Transform initPoint = null;
    [SerializeField] private Transform cpuBattlePoint = null;
    [SerializeField] private Transform onlineBattlePoint = null;
    [SerializeField] private Transform libraryPoint = null;
    [SerializeField] private Transform experimentPoint = null;
    [SerializeField] private MainMenuCameraController cameraController = null;
    [SerializeField] private float cameraSpeed = 5.0f;

    private string nextScene = "";
    private bool isGoNextScene = false;

    private void Start()
    {
        InitCameraPoint();
    }

    public void InitCameraPoint()
    {
        cameraController.SetCameraTransform(initPoint);
    }

    // カメラを初期位置に戻す
    public void SwitchInitPoint()
    {
        StartCoroutine(cameraController.MoveCameraToTransform(initPoint));
    }

    // OnlineBattle
    public void SwitchOnlineBattle()
    {
        StartCoroutine(cameraController.MoveCameraToTransform(onlineBattlePoint));
        
    }

    // VS Cpu
    public void SwitchTrainingBattle()
    {
        StartCoroutine(cameraController.MoveCameraToTransform(cpuBattlePoint));
        isGoNextScene = true;
        nextScene = "Training";
    }


    // 実験場
    public void SwitchExperimentMode()
    {
        StartCoroutine(cameraController.MoveCameraToTransform(experimentPoint));
    }

    // スキル図鑑
    public void SwitchLibraryMode()
    {
        StartCoroutine(cameraController.MoveCameraToTransform(libraryPoint));
    }


    private void Update()
    {
        if (isGoNextScene && !cameraController.isMovingCamera)
        {
            SceneManager.LoadScene(nextScene);
        }
    }

}

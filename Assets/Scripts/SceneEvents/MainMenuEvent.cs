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

    [SerializeField] private GameObject GoTitlePanel;

    private string nextScene = "";
    private bool isGoNextScene = false;

    private void Start()
    {
        InitCameraPoint();
        nextScene = "";
        isGoNextScene = false;

        GoTitlePanel.SetActive(false);
    }

    // タイトル遷移確認画面を表示
    public void OpenGoTitlePanel()
    {
        GoTitlePanel.SetActive(true);


    }

    // タイトル画面に遷移
    public void GoTitleScene()
    {
        CloseGoTitlePanel();
        SceneManager.LoadScene("Title");
    }

    // タイトル遷移確認画面を閉じる
    public void CloseGoTitlePanel()
    {
        GoTitlePanel.SetActive(false);
    }

    public void InitCameraPoint()
    {
        cameraController.SetCameraTransform(initPoint);
    }

    // カメラを初期位置に戻す
    public void SwitchInitPoint()
    {
        CloseGoTitlePanel();
        StartCoroutine(cameraController.MoveCameraToTransform(initPoint));
    }

    // OnlineBattle
    public void SwitchOnlineBattle()
    {
        CloseGoTitlePanel();
        StartCoroutine(cameraController.MoveCameraToTransform(onlineBattlePoint));
        
    }

    // VS Cpu
    public void SwitchTrainingBattle()
    {
        CloseGoTitlePanel();
        StartCoroutine(cameraController.MoveCameraToTransform(cpuBattlePoint));
        isGoNextScene = true;
        nextScene = "Training";
    }


    // 実験場
    public void SwitchExperimentMode()
    {
        CloseGoTitlePanel();
        StartCoroutine(cameraController.MoveCameraToTransform(experimentPoint));
        isGoNextScene = true;
        nextScene = "Experiment";
    }

    // スキル図鑑
    public void SwitchLibraryMode()
    {
        CloseGoTitlePanel();
        StartCoroutine(cameraController.MoveCameraToTransform(libraryPoint));
        isGoNextScene = true;
        nextScene = "Library";
    }


    private void Update()
    {
        if (isGoNextScene && !cameraController.isMovingCamera)
        {
            isGoNextScene = false;
            SceneManager.LoadScene(nextScene);
        }
    }

}

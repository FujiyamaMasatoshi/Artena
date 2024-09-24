using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LibraryEvent : MonoBehaviour
{
    [SerializeField] private UI_Lib_PanelManager panelManager;

    // カメラ移動
    [SerializeField] private Transform defaultCameraTransform;
    [SerializeField] private Transform startCameraTransform;
    [SerializeField] private GameObject mainCamera;
    [SerializeField] private float eventTime = 1.0f;

    private bool isEventFinished = false;

    private void Start()
    {
        isEventFinished = false;
        
        StartEvent();
    }

    

    private void StartEvent()
    {
        // camera moving
        StartCoroutine(MoveCamera());

        // on enable ui canvas
        StartCoroutine(OnEnableAllCanvas());
    }

    private IEnumerator OnEnableAllCanvas()
    {
        yield return new WaitUntil(() => isEventFinished);
        isEventFinished = false;
        panelManager.InitializeAllUI(); // ui canvasの初期化
    }

    private IEnumerator MoveCamera()
    {

        // カメラの位置を初期化
        SetCameraTransform(startCameraTransform);

        float timer = 0f;
        while (timer < eventTime)
        {
            timer += Time.deltaTime;

            mainCamera.transform.position = Vector3.Lerp(startCameraTransform.position, defaultCameraTransform.transform.position, timer / eventTime);
            mainCamera.transform.rotation = Quaternion.Lerp(startCameraTransform.rotation, defaultCameraTransform.rotation, timer / eventTime);
            mainCamera.transform.localScale = Vector3.Lerp(startCameraTransform.localScale, defaultCameraTransform.localScale, timer / eventTime);

            // 1フレーム飛ばす
            yield return null;
        }

        // defaultの位置にカメラをセット
        SetCameraTransform(defaultCameraTransform);

        // イベント終了フラグを立てる
        isEventFinished = true;
    }

    // カメラのTransformをセットする
    private void SetCameraTransform(Transform transform)
    {
        mainCamera.transform.position = transform.position;
        mainCamera.transform.rotation = transform.rotation;
        mainCamera.transform.localScale = transform.localScale;
    }


}

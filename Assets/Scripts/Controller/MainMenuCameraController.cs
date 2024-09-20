using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuCameraController : MonoBehaviour
{
    [SerializeField] GameObject mainCamera = null;
    [SerializeField, Header("0番目は初期化ポイントを入れること")] List<Transform> cameraRotatePoints = new List<Transform>();
    [SerializeField] private float moveTime = 3.0f;

    // 出発したpoint
    private Transform leftPoint = null;
    private Transform nextPoint = null;
    private int leftPointIndex = 0;

    // 移動意中の進捗度合いを管理する変数
    float timer = 0.0f;

    // イベント中かどうかを示すフラグ
    public bool isEventDoing = false;
    public bool isMovingCamera = false; //カメラが動いているかどうか

    private void Start()
    {
        InitCameraTransform();
    }

    private void Update()
    {
        if (!isEventDoing)
        {
            MoveCamera();
        }
    }


    // あるTransformに向けてカメラを移動させる
    public IEnumerator MoveCameraToTransform(Transform point)
    {
        // イベント中のフラグを立てる
        isEventDoing = true;
        isMovingCamera = true;

        var distance = Vector3.Distance(point.position, mainCamera.transform.position);
        var startTransform = mainCamera.transform;

        // カメラをスタート地点に移動させる
        SetCameraTransform(startTransform);

        var expectTime = moveTime;
        Debug.Log($"distance: {distance}, expectTime: {expectTime}");
        timer = 0f;

        Debug.Log($"timer/expectTime: {timer / expectTime}");
        while (true)
        {
            // timerを進める
            timer += Time.deltaTime;
            Debug.Log($"timer: {timer}");

            // Transfromを更新
            mainCamera.transform.position = Vector3.Lerp(startTransform.position, point.position, timer / expectTime);
            mainCamera.transform.rotation = Quaternion.Lerp(startTransform.rotation, point.rotation, timer / expectTime);
            mainCamera.transform.localScale = Vector3.Lerp(startTransform.localScale, point.localScale, timer / expectTime);

            yield return new WaitForSeconds(Time.deltaTime); //1フレーム進める

            if (Vector3.Distance(mainCamera.transform.position, point.position) < 0.1f)
            {
                isMovingCamera = false;
                break;
            }

        }


    }


    // イベント開始フラグを立てる
    public void OnIsEventDoing()
    {
        isEventDoing = true;
    }

    // イベント中のフラグを下ろす
    public void OffIsEventDoing()
    {
        isEventDoing = false;
    }


    // カメラの位置の初期化
    private void InitCameraTransform()
    {
        if (cameraRotatePoints.Count > 3)
        {
            SetCameraTransform(cameraRotatePoints[0]);
            leftPoint = cameraRotatePoints[0];
            nextPoint = cameraRotatePoints[1];

            leftPointIndex = 0;

            timer = 0.0f;
        }
    }


    private void MoveCamera()
    {
        if (cameraRotatePoints.Count > 3)
        {
            timer += Time.deltaTime;

            float progress = Mathf.Clamp01(timer/moveTime);


            // Transfromを更新
            mainCamera.transform.position = Vector3.Lerp(leftPoint.position, nextPoint.position, progress);
            mainCamera.transform.rotation = Quaternion.Lerp(leftPoint.rotation, nextPoint.rotation, progress);
            mainCamera.transform.localScale = Vector3.Lerp(leftPoint.localScale, nextPoint.localScale, progress);

            bool isGetPlace = (Vector3.Distance(mainCamera.transform.position, nextPoint.position) < 0.1f);

            // progress >= 1の場合は、次のポイントの設定をし直す
            if (progress >= 1.0f || isGetPlace)
            {
                // 0<=index<list.Countの時
                if (leftPointIndex < cameraRotatePoints.Count-2)
                {
                    leftPointIndex = leftPointIndex+1;

                    leftPoint = cameraRotatePoints[leftPointIndex];
                    nextPoint = cameraRotatePoints[leftPointIndex+1];
                }
                else
                {
                    leftPointIndex = cameraRotatePoints.Count-1;
                    leftPoint = cameraRotatePoints[leftPointIndex];
                    nextPoint = cameraRotatePoints[0];
                    leftPointIndex = -1;
                }
                // timerのリセット
                timer = 0f;
            }
        }
        
    }

    public void SetCameraTransform(Transform transform)
    {
        mainCamera.transform.position = transform.position;
        mainCamera.transform.rotation = transform.rotation;
        mainCamera.transform.localScale = transform.localScale;
    }


}

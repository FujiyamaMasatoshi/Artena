using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillGeneratorEvent : MonoBehaviour
{
    [SerializeField] private GameObject mainCamera = null;
    [SerializeField] private Transform startCameraPoint = null;
    [SerializeField] private Transform goalCameraPoint = null;
    [SerializeField] private Transform avatarInstPoint = null;
    [SerializeField] private GameObject offlineAvatar_NonCtrl = null;
    [SerializeField] private float cameraSpeed = 5f;

    public bool isFirstEventFinished = false;


    private void Start()
    {
        // イベント開始のフラグを立てる
        isFirstEventFinished = false;

        // カメラをactive falseにする
        mainCamera.gameObject.SetActive(false);
        StartCoroutine(InstantiateAvatar());

        // カメラをactive trueにする
        mainCamera.gameObject.SetActive(true);
        StartCoroutine(CameraWork());

        // first event finish!!
        isFirstEventFinished = true;
    }


    private void SetCameraTransform(Transform startTransform)
    {
        mainCamera.transform.position = startTransform.position;
        mainCamera.transform.rotation = startTransform.rotation;
    }

    // AvatarをInstantiateする
    private IEnumerator InstantiateAvatar()
    {
        var instObj = Instantiate(offlineAvatar_NonCtrl, avatarInstPoint.position, Quaternion.identity);
        instObj.transform.rotation = avatarInstPoint.transform.rotation;
        yield return new WaitForSeconds(1.0f);
    }

    // カメラの動きをここに記述
    private IEnumerator CameraWork()
    {
        var distance = Vector3.Distance(startCameraPoint.position, goalCameraPoint.position);
        var expectTime = distance / cameraSpeed;

        float timer = 0f;

        while (timer < expectTime)
        {
            timer += Time.deltaTime;

            // 線形保管を使用して位置を更新
            mainCamera.transform.position = Vector3.Lerp(startCameraPoint.position, goalCameraPoint.position, timer / expectTime);

            yield return null;
        }

        // 最終位置を確定
        SetCameraTransform(goalCameraPoint);
    }
}

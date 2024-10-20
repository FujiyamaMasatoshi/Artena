using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// あるタイミングで、defaultCameraTransformからcutInCameraTransformに移動させて、
// CutInPanelを表示
// その後、defaultCameraTransformにカメラを戻す
public class CutInEvent : MonoBehaviour
{
    [SerializeField] private float moveCutInTransformTime = 0.5f;
    [SerializeField] private float moveDefaultTransformTime = 0.5f;
    [SerializeField] private float displayTime = 2f;

    [SerializeField] private GameObject mainCamera;
    [SerializeField] private Transform defaultCameraTransform;
    [SerializeField] private Transform cutInCameraTransform;

    [SerializeField] private UI_TB_CutInPanel cutInPanel;

    public bool isFinishCutInEvent = false;

    private bool isFinishCameraMoving = false;

    public void InitCutInEvent()
    {
        cutInPanel.ClearCutInPanel();
        cutInPanel.gameObject.SetActive(false);

        isFinishCutInEvent = false;
        isFinishCameraMoving = false;
    }

    // カットインを実行
    public IEnumerator DoCutIn(SkillType type)
    {
        // カットインイベント開始のフラグ
        isFinishCutInEvent = false;
        StartCoroutine(MoveCamera(defaultCameraTransform, cutInCameraTransform, moveCutInTransformTime));

        yield return new WaitUntil(() => isFinishCameraMoving);
        isFinishCameraMoving = false;

        // panelを表示
        cutInPanel.gameObject.SetActive(true);
        cutInPanel.SetCutInPanel(type);
        yield return new WaitForSeconds(displayTime);


        // カットインイベント終了のフラグ
        isFinishCutInEvent = true;
    }

    public void ResetFlag()
    {
        isFinishCutInEvent = false;
    }

    public IEnumerator EndCutIn()
    {
        isFinishCutInEvent = false;
        cutInPanel.ClearCutInPanel();
        cutInPanel.gameObject.SetActive(false);
        StartCoroutine(MoveCamera(cutInCameraTransform, defaultCameraTransform, moveDefaultTransformTime));

        yield return new WaitUntil(() => isFinishCameraMoving);
        isFinishCameraMoving = false;

        isFinishCutInEvent = true;
    }

    private IEnumerator MoveCamera(Transform startTransform, Transform endTransform, float moveTime)
    {
        isFinishCameraMoving = false;

        //var distance = Vector3.Distance(startTransform.position, endTransform.position);
        mainCamera.transform.position = startTransform.position;
        mainCamera.transform.rotation = startTransform.rotation;

        float timer = 0.0f;

        while (timer < moveTime)
        {
            timer += Time.deltaTime;
            mainCamera.transform.position = Vector3.Lerp(startTransform.position, endTransform.position, timer / moveTime);
            mainCamera.transform.rotation = Quaternion.Lerp(startTransform.rotation, endTransform.rotation, timer / moveTime);

            yield return null;
        }

        // 最終地点にセット
        mainCamera.transform.position = endTransform.position;
        mainCamera.transform.rotation = endTransform.rotation;


        isFinishCameraMoving = true;
    }




}

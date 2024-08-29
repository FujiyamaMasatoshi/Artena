using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class PlayerController : MonoBehaviourPunCallbacks
{
    

    [SerializeField, Header("移動スピード")] private float moveSpeed = 5.0f;
    [SerializeField, Header("回転スピード")] private float rotateSpeed = 500f;
    [SerializeField, Header("ジャンプの大きさ")] private float jumpForce = 100f;
    [SerializeField, Header("Body")] private GameObject body = null;

    [SerializeField, Header("アニメーター")] private Animator anim = null;

    private void Update()
    {
        if (photonView.IsMine)
        {
            Moving();
        }
    }

    private void Moving()
    {
        // wasd入力を受け取る
        var forward = Input.GetAxis("Vertical");
        var right = Input.GetAxis("Horizontal");

        var direction = (transform.forward*forward + transform.right * right).normalized;

        // 移動
        transform.localPosition += direction * moveSpeed * Time.deltaTime;

        // アニメーションへの適用
        var animationSpeed = direction.magnitude * moveSpeed;
        anim.SetFloat("Speed", animationSpeed, 0.1f, Time.deltaTime);


        // bodyの回転
        if (forward > 0)
        {
            body.transform.localScale = new Vector3(1f, 1f, 1f);
        }
        else if (forward < 0)
        {
            body.transform.localScale = new Vector3(1f, 1f, -1f);
        }

        if (right != 0)
        {
            body.transform.localEulerAngles = new Vector3(0f, body.transform.localScale.z * right * 90f, 0f);
        }


    }


}




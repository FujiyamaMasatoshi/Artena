using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Expt_Trash : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("SkillUI"))
        {
            Debug.Log("衝突");
            GameObject colObj = collision.gameObject;
            Destroy(colObj);
        }
    }
}

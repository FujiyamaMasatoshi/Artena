using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Training Battleでのゲームマネージャー
public class TB_GameManager : MonoBehaviour
{
    public static TB_GameManager instance = null;

    public int battlerSkillPoints = 0;
    public int cpuSkillPoints = 0;




    // startの前に呼び出される
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

    }

    public void InitGameData()
    {
        battlerSkillPoints = 0;
        cpuSkillPoints = 0;
    }

    public bool IsWin()
    {
        if (battlerSkillPoints >= cpuSkillPoints) return true;
        else return false;
    }
    
}

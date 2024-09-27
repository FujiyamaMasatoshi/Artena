using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleBgmManager : MonoBehaviour
{
    [SerializeField] private Bgm battleBgm;
    [SerializeField] private Bgm winBgm;
    [SerializeField] private Bgm loseBgm;

    [SerializeField] private Game game;

    private void Start()
    {
        InitSetBGM();
    }

    public void InitSetBGM()
    {
        SetBGM(battleBgm);
    }

    public void SetBGM_GameEnd()
    {
        bool isWin = TB_GameManager.instance.IsWin();
        Debug.Log($"isWin: {isWin}");
        if (isWin) SetBGM(winBgm);
        else SetBGM(loseBgm);
    }

    public void SetBGM(Bgm bgm)
    {
        Debug.Log("bgm: " + bgm.bgm);
        Debug.Log("SM.instance: " + SoundManager.instance);
        Debug.Log($"SM.currentBgmSource: {SoundManager.instance.currentBgmSource}");
        Debug.Log("SM.currentBgmSource.clip: " + SoundManager.instance.currentBgmSource.clip);

        SoundManager.instance.currentBgmSource.Stop();
        SoundManager.instance.currentBgmSource.clip = bgm.bgm;
        SoundManager.instance.currentBgmSource.Play();
    }



}

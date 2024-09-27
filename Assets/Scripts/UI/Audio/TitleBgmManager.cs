using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleBgmManager : MonoBehaviour
{
    [SerializeField] private Bgm titleBgm;

    // Start is called before the first frame update
    void Start()
    {
        InitBgm();
    }

    private void InitBgm()
    {

        SetBGM(titleBgm);
    }

    private void SetBGM(Bgm bgm)
    {
        SoundManager.instance.currentBgmSource.Stop();
        SoundManager.instance.currentBgmSource.clip = bgm.bgm;
        SoundManager.instance.currentBgmSource.Play();
    }
}

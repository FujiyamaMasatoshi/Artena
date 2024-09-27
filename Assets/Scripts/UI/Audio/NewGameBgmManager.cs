using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewGameBgmManager : MonoBehaviour
{

    [SerializeField] private Bgm newGameBgm;

    // Start is called before the first frame update
    void Start()
    {
        InitBgm();
    }

    private void InitBgm()
    {

        SetBGM(newGameBgm);
    }

    private void SetBGM(Bgm bgm)
    {
        SoundManager.instance.currentBgmSource.Stop();
        SoundManager.instance.currentBgmSource.clip = bgm.bgm;
        SoundManager.instance.currentBgmSource.Play();
    }
}

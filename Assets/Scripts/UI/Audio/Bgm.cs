using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bgm : MonoBehaviour
{
    public AudioClip bgm;
    public string scene = "";


    
    public void SetCurrentBGM()
    {
        SoundManager.instance.currentBgmSource.clip = bgm;

    }
}





using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance = null;

    public AudioSource currentBgmSource;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        Debug.Log("call this");
        currentBgmSource = GetComponent<AudioSource>();
        Debug.Log($"currentBgmSource: {currentBgmSource}");
    }

    // 効果音を1回鳴らす
    public void PlayOneShotSE(AudioClip se, float vol=1.0f)
    {
        if (currentBgmSource != null) currentBgmSource.PlayOneShot(se, vol);
    }


}

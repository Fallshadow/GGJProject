using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class AudioPlayMgr : SingletonMonoBehavior<AudioPlayMgr>
{
    public AudioClip[] clips;
    public AudioSource audioSource;
    public float duration = 1f;
    public int curBGMIndex = 0;
    public float BGMvolume = 0.085f;
    public float jokeVolume = 1f;
    public void PlayBGM(int index)
    {
        curBGMIndex = index;
        audioSource.DOFade(0,duration).onComplete = ChangeBGM;

    }
    public void ChangeBGM()
    {

        audioSource.DOFade(BGMvolume,duration);
        audioSource.clip = clips[curBGMIndex];
        audioSource.loop = true;
        audioSource.Play();
    }
    public void StopBgm()
    {
        audioSource.DOFade(0,duration).onComplete = ()=>{audioSource.Pause();};
        
    }
    public void PlayJoke(int index)
    {
        curBGMIndex = index;
        audioSource.volume = jokeVolume;
        audioSource.clip = clips[curBGMIndex];
        audioSource.PlayOneShot(audioSource.clip);

    }
}

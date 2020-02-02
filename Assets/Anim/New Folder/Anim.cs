using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Anim : MonoBehaviour
{
    public AudioClip[] audioClips;
    public AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
        public void StartNextScene()
    {
        LevelMgr.instance.LoadNectLevel();
    }
    public void PlayAudio(int index)
    {
        // if(index == 0 || index == 2)
        // {
        //     audioSource.volume = 0.5f;
        // }
        // else
        // {
        //     audioSource.volume = 0.2f;
        // }
        audioSource.PlayOneShot(audioClips[index]);
    }
}

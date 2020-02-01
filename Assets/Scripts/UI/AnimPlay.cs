using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimPlay : SingletonMonoBehaviorNoDestroy<AnimPlay>
{
    private Animator animator;
    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start()
    {
        animator = GetComponent<Animator>();
    }
    public void PlayOutScene()
    {
        animator.Play("OutScene");
    }
    public void PlayInScene()
    {
        animator.Play("InScene");
    }

    public void StartNextScene()
    {
        LevelMgr.instance.LoadNectLevel();
    }
}

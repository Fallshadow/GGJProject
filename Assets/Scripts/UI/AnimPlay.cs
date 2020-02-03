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
        animator = GetComponentInChildren<Animator>();
    }
    public void PlayOutScene()
    {
        animator.Play("OutScene");
    }
    public void PlayInScene()
    {
        animator.Play("InScene");
    }
    public void PlayBadEnd1()
    {
        animator.Play("BadEnd1");
    }
    public void PlayName()
    {
        animator.Play("Name");
    }
    public void PlayGoodName()
    {
        animator.Play("goodend");
    }
}

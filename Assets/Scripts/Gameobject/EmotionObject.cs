using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum EmotionType
{
    ET_SPEAK = 0,
    ET_THINK,
    ET_MEMORY,
    ET_EMOTION,
}
public class EmotionObject : MonoBehaviour
{
    public string emotionName = "";
    public EmotionType et = EmotionType.ET_SPEAK;
    private bool _added;
    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake()
    {
        if (CommendMgr.instance.emotionlist.Count < (int)et)
        {
            Destroy(gameObject);
        }
        _added = false;
    }
    /// <summary>
    /// Sent when an incoming collider makes contact with this object's
    /// collider (2D physics only).
    /// </summary>
    /// <param name="other">The Collision2D data associated with this collision.</param>
    void OnCollisionEnter2D(Collision2D other)
    {
        if (!_added && other.transform.tag == "Player")
        {
            bool canAdd = false;
            switch (et)
            {
                case EmotionType.ET_SPEAK:
                    canAdd = CommendMgr.instance.AddEmotionMode("speak");
                    break;
                case EmotionType.ET_THINK:
                    canAdd = CommendMgr.instance.AddEmotionMode("think");
                    break;
                case EmotionType.ET_MEMORY:
                    canAdd = CommendMgr.instance.AddEmotionMode("memory");
                    break;
                case EmotionType.ET_EMOTION:
                    canAdd = CommendMgr.instance.AddEmotionMode("emotion");
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            if (canAdd)
            {
                Dialog.instance.ExecuteBlock((int)LevelMgr.instance.curLevel + "-computer");
                _added = true;
            }
            else
            {
                Dialog.instance.ExecuteBlock("grooveFull");
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum EmotionType
{
    ET_THINK = 0,
    ET_MEMORY ,
    ET_EMOTION ,
}
public class EmotionObject : MonoBehaviour
{
    public string emotionName = "";
    public EmotionType et = EmotionType.ET_THINK;

    /// <summary>
    /// Sent when an incoming collider makes contact with this object's
    /// collider (2D physics only).
    /// </summary>
    /// <param name="other">The Collision2D data associated with this collision.</param>
    void OnCollisionEnter2D(Collision2D other)
    {
        if(other.transform.tag == "Player" ){
        }
    }
}

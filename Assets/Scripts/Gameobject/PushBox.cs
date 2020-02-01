
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushBox : MonoBehaviour
{
    public bool isPsuh = false;
    /// <summary>
    /// Sent when an incoming collider makes contact with this object's
    /// collider (2D physics only).
    /// </summary>
    /// <param name="other">The Collision2D data associated with this collision.</param>
    
    void OnCollisionEnter2D(Collision2D other)
    {
        if(other.transform.tag == "Player" )
        {
            isPsuh = true;
            EventManager.instance.Send<bool>(EventGroup.GAME,(short)GameEvent.isPushBox,isPsuh);
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if(other.transform.tag == "Player" )
        {
            isPsuh = false;
            EventManager.instance.Send<bool>(EventGroup.GAME,(short)GameEvent.isPushBox,isPsuh);
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceDieBox : MonoBehaviour
{
    public float force = 50f;
    /// <summary>
    /// Sent when an incoming collider makes contact with this object's
    /// collider (2D physics only).
    /// </summary>
    /// <param name="other">The Collision2D data associated with this collision.</param>
    
    void OnCollisionEnter2D(Collision2D other)
    {
        if(other.transform.tag == "Player" && other.transform.GetComponent<Player>().isAlive)
        {
            FindObjectOfType<AnimScript>().SetDieParam();
            other.transform.GetComponent<Player>().isAlive = false;
            Debug.Log(other.contacts[0].normal);
            other.transform.GetComponent<Rigidbody2D>().velocity +=  other.contacts[0].normal*(-force);
            CoreGameMgr.instance.hadDead = true;

        }
    }
}


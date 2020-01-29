using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private MoveFunction moveFunction = null;
    void Awake()
    {
        moveFunction.Init(GetComponent<Rigidbody2D>());
    }
}

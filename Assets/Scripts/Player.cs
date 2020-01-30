﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MoveFunction))]
public class Player : MonoBehaviour
{
    public float x = 0;
    public float y = 0;
    public float xRaw = 0;
    public float yRaw = 0;
    public bool isFreeControl = true;
    private MoveFunction moveFunction = null;
    void Awake()
    {
        DontDestroyOnLoad(this);
        moveFunction = GetComponent<MoveFunction>();
        moveFunction.Init(
            GetComponent<Rigidbody2D>(),
            GetComponentInChildren<AnimScript>(),
            GetComponentInChildren<Collion>(),
            GetComponentInChildren<BetterJump>()
            );
    }

    void Update()
    {
        x = Input.GetAxis("Horizontal");
        y = Input.GetAxis("Vertical");
        xRaw = Input.GetAxisRaw("Horizontal");
        yRaw = Input.GetAxisRaw("Vertical");
        Vector2 dir = new Vector2(x, y);
        moveFunction.ReSide(x);
        moveFunction.SetCommonThing();

        if (isFreeControl)
        {
            moveFunction.Walk(dir);
            if (Input.GetButtonDown("Jump"))
            {
                moveFunction.Jump(Vector2.up);
            }
            if (Input.GetButton("Fire3"))
            {
                moveFunction.GrabWall();
            }
            if (Input.GetButtonDown("Fire1"))
            {
                if(xRaw != 0 || yRaw != 0)
                    moveFunction.Dash(new Vector2(xRaw,yRaw));
            }
        }
        else
        {

        }
        moveFunction.SetCollision(x,y);
    }
}

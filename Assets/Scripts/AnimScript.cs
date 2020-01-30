using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimScript : MonoBehaviour
{
    private Animator animator = null;
    private SpriteRenderer spriteRenderer = null;
    [SerializeField] private int side = 1;
    void Awake()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    public void SetMoveParam(float x, float y)
    {
        animator.SetFloat("HorizontalAxis", x);
        animator.SetFloat("VerticalAxis", y);
    }

    public void SetCommonParam(float yVel)
    {
        animator.SetFloat("VerticalSpeed", yVel);
    }

    public void SetCollisionParam(bool onGround,bool onWall)
    {
        animator.SetBool("OnGround",onGround);
        animator.SetBool("OnWall",onWall);
    }
    public void SetJumpParam()
    {
        animator.SetTrigger("JumpTrg");
    }
    public void SetDashParam()
    {
        animator.SetTrigger("DashTrg");
    }
    public void SetGrabParam(bool OnWallGrab,bool OnWallWalk)
    {
        animator.SetBool("OnWallGrab",OnWallGrab);
        animator.SetBool("OnWallWalk",OnWallWalk);
    }

    public void SetSide(int side)
    {
        if(side != 0)
        {
            this.side = side;
        }
        spriteRenderer.flipX = this.side == 1 ? false : true;
    }
}

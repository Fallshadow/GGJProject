using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

/// <summary>
/// 将所有的移动功能放在此处（动画亦是如此）
/// </summary>
public class MoveFunction : MonoBehaviour
{
    [Space]
    [Header("移动功能开关")]
    public bool canMoveTRG = true;
    public bool canMoveLeftTRG = true;
    public bool canMoveRightTRG = true;
    public bool canJumpTRG = true;
    public bool canDashTRG = true;
    public bool canGrabTRG = true;
    public bool canGrabLeftTRG = true;
    public bool canGrabRightTRG = true;
    public bool canDashUni = false;

    [Space]
    [Header("移动功能参数")]
    public float speed = 10;
    public float jumpForce = 50;
    public float slideSpeed = 5;
    public float dashSpeed = 20;
    public float jumpParam = 0.8f;
public float waittime = 0.3f;

    [Space]
    [Header("当前状态")]
    public bool canMove = true;
    public bool OnWallGrab = false;
    public bool OnWallWalk = false;
    public bool isDashing = false;
    public bool hasDashed = false;
    public bool groundTouch = false;
    public bool isSlider = false;
    public bool walljump = false;

    private Rigidbody2D rb = null;
    private Collion coll = null;
    private AnimScript animScript;
    private BetterJump betterJump;

    /// <summary>
    /// 初始化
    /// </summary>
    /// <param name="rb"></param>
    /// <param name="animScript"></param>
    public void Init(Rigidbody2D rb, AnimScript animScript, Collion coll,BetterJump betterJump)
    {
        this.rb = rb;
        this.animScript = animScript;
        this.coll = coll;
        this.betterJump = betterJump;
        ResetFunction();
    }

    /// <summary>
    /// walk功能
    /// </summary>
    /// <param name="dir"></param>
    public void Walk(Vector2 dir)
    {
        if (!canMoveTRG)
        {
            return;
        }
        if (isDashing)
        {
            return;
        }
        if (!canMoveLeftTRG)
        {
            dir.x = dir.x < 0 ? 0 : dir.x;
        }
        if (!canMoveRightTRG)
        {
            dir.x = dir.x > 0 ? 0 : dir.x;
        }
        rb.velocity = new Vector2(dir.x * speed, rb.velocity.y);
        animScript.SetMoveParam(dir.x, dir.y);
    }

    /// <summary>
    /// jump功能
    /// </summary>
    /// <param name="dir"></param>
    public void Jump(Vector2 dir)
    {
        if (!coll.onGround || !canJumpTRG)
        {
            if(!coll.onWall)
            return;
        }
        if(!coll.onGround && coll.onWall)
        {
                    StopCoroutine(DisableMovement(0));
        StartCoroutine(DisableMovement(waittime));
            rb.velocity = new Vector2(0, 0);
            rb.velocity += (Vector2.up / jumpParam + (new Vector2(-(coll.onRightWall? 1: -1),0)) / jumpParam) * jumpForce;
            animScript.SetJumpParam();
            animScript.SetSide(-(int)myside);
            walljump = true;
        }
        else{
        rb.velocity = new Vector2(rb.velocity.x, 0);
        rb.velocity += dir * jumpForce;
        animScript.SetJumpParam();
        }
    }
    IEnumerator DisableMovement(float time)
    {
        canMove = false;
        yield return new WaitForSeconds(time);
        canMove = true;
    }
    /// <summary>
    /// GrabWall功能
    /// </summary>
    public void GrabWall()
    {
        if (!coll.onWall || !canGrabTRG)
        {
            return;
        }
        ReSide(coll.wallSide);
        OnWallGrab = true;
    }
    /// <summary>
    /// DASH功能
    /// </summary>
    public void Dash(Vector2 dir)
    {
        if(!canDashTRG || hasDashed || isDashing)
        {
            if(!canDashUni || isDashing)
            {
                return;
            }
            
        }
        Camera.main.transform.DOComplete();
        Camera.main.transform.DOShakePosition(.2f, .5f, 14, 90, false, true);
        FindObjectOfType<RippleEffect>().Emit(Camera.main.WorldToViewportPoint(transform.position));
        FindObjectOfType<GhostTrail>().ShowGhost();

        hasDashed = true;

        animScript.SetDashParam();
        rb.velocity = Vector2.zero;
        rb.velocity += dir.normalized * dashSpeed;
        StopCoroutine(_dashWait());
        StartCoroutine(_dashWait());
    }
    IEnumerator _dashWait()
    {
        StopCoroutine(_refHasDash());
        StartCoroutine(_refHasDash());
        DOVirtual.Float(14, 0, .8f, RigidbodyDrag);
        rb.gravityScale = 0;
        isDashing = true;
        betterJump.enabled = false;
        animScript.PlayAudio(0);
        yield return new WaitForSeconds(.3f);
        rb.gravityScale = 3;
        isDashing = false;
        betterJump.enabled = true;
    }
    void RigidbodyDrag(float x)
    {
        rb.drag = x;
    }

    IEnumerator _refHasDash()
    {
        yield return new WaitForSeconds(.15f);
        if (coll.onGround)
        {
            hasDashed = false;
        }
    }

    public float myside = 0;
    /// <summary>
    /// 转换方向
    /// </summary>
    public void ReSide(float x)
    {
        int side = 0;
        if (x > 0)
        {
            side = 1;
            myside = side;
        }
        if (x < 0)
        {
            side = -1;
            myside = side;
        }
        
        animScript.SetSide(side);
    }

    public void SetCommonThing()
    {
        OnWallGrab = false;
        SetCommonParam();
    }

    /// <summary>
    /// 碰撞体监测响应，自然
    /// </summary>
    public void SetCollision(float x,float y)
    {
        coll.isDashing = isDashing;
        if (coll.onGround && !groundTouch)
        {
            hasDashed = false;
            groundTouch = true;
            animScript.PlayAudio(1);
        }
        if(!coll.onGround && groundTouch)
        {
            groundTouch = false;
        }

        if (coll.onGround && !isDashing)
        {
            betterJump.enabled = true;
        }
        if (coll.onWall && !coll.onGround)
        {
            if (x != 0)
            {
                WallSlide();
            }
            else
            {
                isSlider = false;
            }
        }
        OnWallWalk = y != 0 ? true : false;
        if (OnWallGrab)
        {
            rb.gravityScale = 0;
            float speedModifier = y > 0 ? .5f : 1;

            rb.velocity = new Vector2(rb.velocity.x, y * (speed * speedModifier));
        }
        else
        {
            
            rb.gravityScale = 3;
        }
        animScript.SetGrabParam(OnWallGrab,OnWallWalk);
    }

    /// <summary>
    /// 地面、墙体判断,Y方向速度
    /// </summary>
    public void SetCommonParam()
    {
        animScript.SetCollisionParam(coll.onGround, coll.onWall);
        animScript.SetCommonParam(rb.velocity.y);
    }

    public void ResetFunction()
    {
        canMoveTRG = true;
        canMoveLeftTRG = true;
        canMoveRightTRG = true;
        canJumpTRG = true;
        canDashTRG = true;
        canGrabTRG = true;
        canGrabLeftTRG = true;
        canGrabRightTRG = true;
    }
    private void WallSlide()
    {
        if(walljump)
        {
            walljump = false;
            return ;
        }
        bool pushingWall = false;
        if ((rb.velocity.x > 0 && coll.onRightWall) || (rb.velocity.x < 0 && coll.onLeftWall))
        {
            pushingWall = true;
            isSlider = true;
        }
        float push = pushingWall ? 0 : rb.velocity.x;

        rb.velocity = new Vector2(push, -slideSpeed);
        
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collion : MonoBehaviour
{
    [Header("Layers")]
    public LayerMask groundLayer;

    [Space]

    [Header("碰撞监测")]
    public bool onGround = false;
    public bool onWall = false;
    public bool onLeftWall = false;
    public bool onRightWall = false;
    public int wallSide = 1;
    [Space]

    [Header("Collision")]

    public float collisionRadius = 0.25f;
    public Vector2 bottomOffset, rightOffset, leftOffset;
    private Color debugCollisionColor = Color.red;
    [Header("PlayerState")]
    public bool isDashing = false;
    void Update()
    {
        onGround = Physics2D.OverlapCircle((Vector2)transform.position + bottomOffset, collisionRadius, groundLayer);
        onWall = Physics2D.OverlapCircle((Vector2)transform.position + rightOffset, collisionRadius, groundLayer) ||
                Physics2D.OverlapCircle((Vector2)transform.position + leftOffset, collisionRadius, groundLayer);
        onLeftWall = Physics2D.OverlapCircle((Vector2)transform.position + leftOffset, collisionRadius, groundLayer);
        onRightWall = Physics2D.OverlapCircle((Vector2)transform.position + rightOffset, collisionRadius, groundLayer);
        wallSide = onRightWall ? 1 : -1;
    }
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        var positions = new Vector2[] { bottomOffset, rightOffset, leftOffset };

        Gizmos.DrawWireSphere((Vector2)transform.position + bottomOffset, collisionRadius);
        Gizmos.DrawWireSphere((Vector2)transform.position + rightOffset, collisionRadius);
        Gizmos.DrawWireSphere((Vector2)transform.position + leftOffset, collisionRadius);
    }


    /// <summary>
    /// Sent when an incoming collider makes contact with this object's
    /// collider (2D physics only).
    /// </summary>
    /// <param name="other">The Collision2D data associated with this collision.</param>
    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.transform.tag == "victoryDoor")
        {
            //LevelMgr.instance.LoadNectLevel();
            if (LevelMgr.instance.curLevel == LEVEL_NAME.LN_LEVEL7)
            {
                Dialog.instance.ExecuteBlock((int)LevelMgr.instance.curLevel + "-" + "end", () => AnimPlay.instance.PlayOutScene());
                return;
            }

            if (LevelMgr.instance.curLevel == LEVEL_NAME.LN_LEVEL6)
            {
                Dialog.instance.ExecuteBlock((int)LevelMgr.instance.curLevel + "-" + "endcom", () =>
                {

                    if (CommendMgr.instance.CheckEmotion())
                    {
                        Dialog.instance.ExecuteBlock((int)LevelMgr.instance.curLevel + "-" + "end", () => AnimPlay.instance.PlayOutScene());
                        return;
                    }
                    else
                    {
                        AnimPlay.instance.PlayOutScene();
                    }


                });


                return;
            }
            if (LevelMgr.instance.curLevel == LEVEL_NAME.LN_LEVEL10)
            {

                    if (CommendMgr.instance.JudgeGoodOrBad())
                    {
                        Dialog.instance.ExecuteBlock("good");
                    }
                    else
                    {
                        Dialog.instance.ExecuteBlock("bad");
                    }

                    if(CoreGameMgr.instance.hadDead)
                    {
                        Dialog.instance.ExecuteBlock("killedRobot");
                    }
                    else
                    {
                        Dialog.instance.ExecuteBlock("DontKilledRobot");
                    }
                    Dialog.instance.ExecuteBlock((int)LevelMgr.instance.curLevel + "-" + "end");
                    AudioPlayMgr.instance.StopBgm();
                    return;
            }
            if (CommendMgr.instance.CheckEmotion())
            {
                if (CommendMgr.instance.JudgeGoodOrBad())
                {
                    Dialog.instance.ExecuteBlock("good");
                }
                else
                {
                    Dialog.instance.ExecuteBlock("bad");
                }
                Dialog.instance.ExecuteBlock((int)LevelMgr.instance.curLevel + "-" + "end", () => AnimPlay.instance.PlayOutScene());
                return;
            }

            AnimPlay.instance.PlayOutScene();
        }
    }



}

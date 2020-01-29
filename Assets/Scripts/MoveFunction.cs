using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveFunction : MonoBehaviour
{
    [Space]
    [Header("移动功能开关")]
    public bool canMoveTRG;
    public bool canJumpTRG;
    public bool canDashTRG;
    public bool canGrabTRG;
    [Space]
    [Header("移动功能参数")]
    public float speed = 10;


    private Rigidbody2D rb = null;
    public void Init(Rigidbody2D rb)
    {
        this.rb = rb;
    }
    public void Walk(Vector2 dir)
    {
        rb.velocity = new Vector2(dir.x * speed, rb.velocity.y);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MoveFunction))]
public class Player : MonoBehaviour
{
    [Header("信号")]
    public bool inputLeft = false;
    public bool inputRight = false;
    public bool inputUp = false;
    public bool inputDown = false;
    public bool inputJump = false;
    public bool inputDash = false;
    public bool inputGrab = false;
    public bool[] playerbools = new bool[7];
    public bool[] playerboolFuncs = new bool[7];
    public float x = 0;
    public float y = 0;
    public float xRaw = 0;
    public float yRaw = 0;
    public float changePoint = 0.9f;
    public bool isFreeControl = true;
    public bool btnJump = false;
    public bool btnDash = false;
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
            inputJump = false;
            inputGrab = false;
            inputDash = false;
            if(xRaw < -changePoint)
            {
                inputLeft = true;
                inputRight = false;
            }
            if(xRaw == 0)
            {
                inputLeft = false;
                inputRight = false;
            }
            if(xRaw > changePoint)
            {
                inputLeft = false;
                inputRight = true;
            }
            if(yRaw > changePoint)
            {
                inputUp = true;
                inputDown = false;
            }
            if(yRaw == 0)
            {
                inputUp = false;
                inputDown = false;
            }
            if(yRaw < -changePoint)
            {
                inputUp = false;
                inputDown = true;
            }
            if (Input.GetButton("Jump"))
            {
                inputJump = true;
                btnJump = true;
            }
            if (Input.GetButtonUp("Jump"))
            {
                inputJump = false;
                btnJump = false;
            }
            if (Input.GetButton("Fire3"))
            {
                inputGrab = true;
            }
            if (Input.GetButton("Fire1"))
            {
                inputDash = true;
                btnDash = true;
            }
            if (Input.GetButtonUp("Fire1"))
            {
                inputDash = false;
                btnDash = false;
            }
            playerbools = new bool[7]
            {
                inputLeft ,
                inputRight,
                inputUp,
                inputDown ,
                inputJump ,
                inputDash ,
                inputGrab ,
            };
            CheckMergeInput();
        moveFunction.ReSide(x);
        moveFunction.SetCommonThing();
            if (inputLeft)
            {
                x = -1;
                xRaw = -1;
            }
            if (inputRight)
            {
                x = 1;
                xRaw = 1;
            }
            if (inputUp)
            {
                y = 1;
                yRaw = 1;
            }
            if (inputDown)
            {
                y = -1;
                yRaw = -1;
            }
            dir = new Vector2(x,y);
            moveFunction.Walk(dir);
            if (inputJump)
            {
                moveFunction.Jump(Vector2.up);
            }
            if (inputGrab)
            {
                moveFunction.GrabWall();
            }
            if (inputDash)
            {
                if(xRaw != 0 || yRaw != 0)
                    moveFunction.Dash(new Vector2(xRaw,yRaw));
            }
        }
        moveFunction.SetCollision(x,y);
    }

    public void CheckMergeInput()
    {
        foreach (var commend in CommendMgr.instance.playerCommends)
        {
            bool hadChange = false;
            foreach (int item in commend)
            {
                playerboolFuncs[item] = true;
            }
            foreach (int item in commend)
            {
                if (playerbools[item] == true)
                {
                    if (item == 4)
                    {
                        if (!btnJump) continue;
                    }
                    if (item == 5)
                    {
                        if (!btnDash)continue;
                    }
                    foreach (int citem in commend)
                    {
                        playerbools[citem] = true;
                    }
                    hadChange = true;
                    break;
                }
            }
            if(!hadChange)
            {
                foreach (int citem in commend)
                {
                    playerbools[citem] = false;
                }
            }
        }
        SetFunc();
        SetBools();
    }

    public void SetBools()
    {
        inputLeft  = playerbools[0];
        inputRight = playerbools[1];
        inputUp    = playerbools[2];
        inputDown  = playerbools[3];
        inputJump  = playerbools[4];
        inputDash  = playerbools[5];
        inputGrab  = playerbools[6];  
    }
    public void SetFunc()
    {

  moveFunction.canMoveLeftTRG          =playerboolFuncs[0];
  moveFunction.canMoveRightTRG         =playerboolFuncs[1];
  moveFunction.canJumpTRG              =playerboolFuncs[4];
  moveFunction.canDashTRG              =playerboolFuncs[5];
  moveFunction.canGrabTRG              =playerboolFuncs[6];
    }
}

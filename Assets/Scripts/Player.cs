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
    public bool downJump = false;
    public bool inputDash = false;

    public bool downDash = false;
    public bool inputGrab = false;
    public bool[] playerbools = new bool[7];
    public bool[] playerboolFuncs = new bool[8];
    public float x = 0;
    public float y = 0;
    public float xRaw = 0;
    public float yRaw = 0;
    public float changePoint = 0.9f;
    public bool isFreeControl = true;
    public bool btnJump = false;
    public bool btnDash = false;
    public bool isAlive = true;
    public bool downBtn = false;
    private MoveFunction moveFunction = null;
    public void ResetPlayer()
    {
        isAlive = true;
    }
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
        if (!moveFunction.canMove)
        {
            return;
        }
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
            if (Input.GetKeyDown(KeyCode.LeftControl))
            {
                if (xRaw != 0 || yRaw != 0)
                    moveFunction.Dash(new Vector2(xRaw, yRaw));
            }
        }
        else
        {
            inputJump = false;
            inputGrab = false;
            inputDash = false;
            if (xRaw < -changePoint)
            {
                inputLeft = true;
                inputRight = false;
            }
            if (xRaw == 0)
            {
                inputLeft = false;
                inputRight = false;
            }
            if (xRaw > changePoint)
            {
                inputLeft = false;
                inputRight = true;
            }
            if (yRaw > changePoint)
            {
                inputUp = true;
                inputDown = false;
            }
            if (yRaw == 0)
            {
                inputUp = false;
                inputDown = false;
            }
            if (yRaw < -changePoint)
            {
                inputUp = false;
                inputDown = true;
            }
            if (Input.GetButton("Jump"))
            {
                downJump = false;
                inputJump = true;
                btnJump = true;
            }
            if (Input.GetButtonDown("Jump"))
            {
                inputJump = true;
                downJump = true;
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
            if (Input.GetKey(KeyCode.LeftControl))
            {
                inputDash = true;
                btnDash = true;
                downDash = false;
            }
            if (Input.GetKeyDown(KeyCode.LeftControl))
            {
                inputDash = true;
                downDash = true;
            }

            if (Input.GetKeyUp(KeyCode.LeftControl))
            {
                inputDash = false;
                btnDash = false;
            }
                                    if(Input.GetKey(KeyCode.W)||Input.GetKey(KeyCode.A)||Input.GetKey(KeyCode.S)||Input.GetKey(KeyCode.D)||
                Input.GetKey(KeyCode.UpArrow)||Input.GetKey(KeyCode.DownArrow)||Input.GetKey(KeyCode.LeftArrow)||Input.GetKey(KeyCode.RightArrow))
                {
                    
                            
                    StartCoroutine(_setDownFalse());
                }
            if(Input.GetKeyDown(KeyCode.W)||Input.GetKeyDown(KeyCode.A)||Input.GetKeyDown(KeyCode.S)||Input.GetKeyDown(KeyCode.D)||
                Input.GetKeyDown(KeyCode.UpArrow)||Input.GetKeyDown(KeyCode.DownArrow)||Input.GetKeyDown(KeyCode.LeftArrow)||Input.GetKeyDown(KeyCode.RightArrow))
                {
                    downBtn = true;
                }
                if(Input.GetKeyUp(KeyCode.W)||Input.GetKeyUp(KeyCode.A)||Input.GetKeyUp(KeyCode.S)||Input.GetKeyUp(KeyCode.D)||
                Input.GetKeyUp(KeyCode.UpArrow)||Input.GetKeyUp(KeyCode.DownArrow)||Input.GetKeyUp(KeyCode.LeftArrow)||Input.GetKeyUp(KeyCode.RightArrow))
                {
                    StopCoroutine(_setDownFalse());
                    downBtn = false;
                }
                IEnumerator _setDownFalse()
                {
                    yield return new WaitForSeconds(.2f);
                    downBtn = false;
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
            x = 0;
            xRaw = 0;
            y = 0;
            yRaw = 0;
            if (inputLeft && playerboolFuncs[0])
            {
                x = -1;
                xRaw = -1;
            }
            if (inputRight && playerboolFuncs[1])
            {
                x = 1;
                xRaw = 1;
            }
            if (inputUp && playerboolFuncs[2])
            {
                y = 1;
                yRaw = 1;
            }
            if (inputDown && playerboolFuncs[3])
            {
                y = -1;
                yRaw = -1;
            }
            if (inputLeft == inputRight && inputRight == true)
            {
                x = 0;
                xRaw = 0;
            }
            if (inputDown == inputUp && inputUp == true)
            {
                x = 0;
                xRaw = 0;
            }
            dir = new Vector2(x, y);
            moveFunction.Walk(dir);
            if (inputJump && downJump)
            {
                moveFunction.Jump(Vector2.up);
            }
            if (inputGrab)
            {
                moveFunction.GrabWall();
            }
            if (inputDash && downDash)
            {
                if (xRaw != 0 || yRaw != 0)
                    moveFunction.Dash(new Vector2(xRaw, yRaw));
            }
        }
        moveFunction.SetCollision(x, y);
    }

    public void CheckMergeInput()
    {


        if (CommendMgr.instance.playerCommends.Count == 0)
        {
            playerbools[0] = false;
            playerbools[1] = false;
            playerbools[2] = false;
            playerbools[3] = false;
            playerbools[4] = false;
            playerbools[5] = false;
            playerbools[6] = false;
            playerboolFuncs[0] = false;
            playerboolFuncs[1] = false;
            playerboolFuncs[2] = false;
            playerboolFuncs[3] = false;
            playerboolFuncs[4] = false;
            playerboolFuncs[5] = false;
            playerboolFuncs[6] = false;
        }
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
                        if (!btnDash) continue;
                    }
                    foreach (int citem in commend)
                    {
                        playerbools[citem] = true;
                        if(citem == 4 && citem != item)
                        {
                            if(!downBtn)
                            {
                                playerbools[citem] = false;
                                downJump = false;
                            }
                            else
                            {
                                downJump = true;
                            }
                        }
                        if(citem == 5 && citem != item)
                        {
                            if(!downBtn)
                            {
                                playerbools[citem] = false;
                                downDash = false;
                            }
                            else
                            {
                                downDash = true;
                            }
                        }
                        
                    }
                    hadChange = true;
                    break;
                }
            }
            if (!hadChange)
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
        inputLeft = playerbools[0];
        inputRight = playerbools[1];
        inputUp = playerbools[2];
        inputDown = playerbools[3];
        inputJump = playerbools[4];
        inputDash = playerbools[5];
        inputGrab = playerbools[6];
    }
    public void SetFunc()
    {

        moveFunction.canMoveLeftTRG = playerboolFuncs[0];
        moveFunction.canMoveRightTRG = playerboolFuncs[1];
        moveFunction.canMoveUpTRG = playerboolFuncs[2];
        moveFunction.canMoveDownTRG = playerboolFuncs[3];
        moveFunction.canJumpTRG = playerboolFuncs[4];
        moveFunction.canDashTRG = playerboolFuncs[5];
        moveFunction.canGrabTRG = playerboolFuncs[6];
        moveFunction.canDashUni = LevelMgr.instance.curStruct.canDashUni;
    }

}

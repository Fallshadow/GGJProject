using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimScript : MonoBehaviour
{
    private Animator animator = null;
    public SpriteRenderer spriteRenderer = null;
    public AudioClip[] audioClips;
    public AudioSource audioSource;
    [SerializeField] private int side = 1;
    
    public void PlayAudio(int index)
    {
        if(index == 0 || index == 2)
        {
            audioSource.volume = 0.5f;
        }
        else
        {
            audioSource.volume = 0.2f;
        }
        audioSource.PlayOneShot(audioClips[index]);
    }
    void Awake()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start()
    {
        EventManager.instance.Register<bool>(EventGroup.GAME,(short)GameEvent.isPushBox,SetPushBox);
        EventManager.instance.Register(EventGroup.GAME,(short)GameEvent.PlayerDie,SetDieParam);
        changeState();
    }

    public void changeState()
    {
        if(LevelMgr.instance.curLevel == LEVEL_NAME.LN_START||LevelMgr.instance.curLevel == LEVEL_NAME.LN_LEVEL1)
        {
            animator.Play("Sleep");
        }
    }
    public void awake()
    {
        animator.SetBool("Awake",true);
    }
        public void sleep()
    {
        animator.SetBool("Awake",false);
    }
    private void OnDestroy() {
        EventManager.instance.Unregister<bool>(EventGroup.GAME,(short)GameEvent.isPushBox,SetPushBox);
        EventManager.instance.Unregister(EventGroup.GAME,(short)GameEvent.PlayerDie,SetDieParam);
    }
    public void SetMoveParam(float x, float y)
    {
        animator.SetFloat("HorizontalAxis", x);
        animator.SetFloat("VerticalAxis", y);
    }
    public void SetAwakeParam()
    {
        animator.SetBool("Awake",true);
    }

    public void SetCommonParam(float yVel)
    {
        animator.SetFloat("VerticalSpeed", yVel);
        animator.SetBool("IsAlive",transform.parent.GetComponent<Player>().isAlive);
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
    public void SetDieParam()
    {
        transform.parent.GetComponent<Player>().isAlive = false;
        animator.SetTrigger("Die");
        
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
    public void SetPushBox(bool ispush)
    {
        animator.SetBool("IsPush",ispush);
    }

    public void Fail()
    {
        CoreGameMgr.instance.Failed();
    }
    public void Victory()
    {
        CoreGameMgr.instance.Victory();
    }
}

using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum LEVEL_NAME
{
    LN_START = 0,
    LN_LEVEL1,
    LN_LEVEL2,
    LN_LEVEL3,
    LN_LEVEL4,
    LN_LEVEL5,
    LN_LEVEL6,
    LN_LEVEL7,
    LN_LEVEL8,
    LN_LEVEL9,
    LN_LEVEL10,
}
public struct LevelStruct
{
    public Vector2 StartPos;
    public int leftTime;
    public int FuncGroove;
    public bool canDashUni;
}
public class LevelMgr : SingletonMonoBehaviorNoDestroy<LevelMgr>
{
    public LEVEL_NAME curLevel = LEVEL_NAME.LN_START;
    public LevelStruct curStruct;
    [HideInInspector]
    public GameObject player = null;
    private void Start()
    {
        player = GameObject.Find("Player");
    }
    public void LoadLevel(LEVEL_NAME level)
    {
        SceneManager.LoadScene((int)level);
        curLevel = level;
        if(level == LEVEL_NAME.LN_LEVEL4)
        AudioPlayMgr.instance.PlayBGM(1);
        if(level == LEVEL_NAME.LN_LEVEL7)
        AudioPlayMgr.instance.PlayBGM(2);
        if(level == LEVEL_NAME.LN_LEVEL10)
        AudioPlayMgr.instance.PlayBGM(3);
    }
public void BadEnd()
{
    player.transform.position = curStruct.StartPos;
    FindObjectOfType<AnimScript>().sleep();
    AnimPlay.instance.PlayName();
    FindObjectOfType<MoveFunction>().canMove = false;
}

    public void LoadNectLevel()
    {
        UIMgr.instance.DestoryAllUi();
        if (curLevel == LEVEL_NAME.LN_LEVEL10)
        {
            curLevel = LEVEL_NAME.LN_START;
            CommendMgr.instance.emotionlist.Clear();
            curLevel = curLevel + 1;
            curStruct = LevelConfig.levelStructs[(int)curLevel];
            UIMgr.instance.GetUI(PrefabPathConfig.MainGameTip);
            RestartCurLevel();
            SceneManager.LoadScene(1);
             CoreGameMgr.instance.hadDead = false;
             
             FindObjectOfType<AnimScript>().sleep();
             FindObjectOfType<AnimScript>().changeState();
            return;
        }
        LEVEL_NAME level = curLevel + 1;
        curLevel = level;
        curStruct = LevelConfig.levelStructs[(int)curLevel];
        if (curLevel != LEVEL_NAME.LN_START)
        {
            UIMgr.instance.GetUI(PrefabPathConfig.MainGameTip);
        }
        else
        {
            UIMgr.instance.GetUI(PrefabPathConfig.Opening);
        }
        //StartCoroutine(_restart());
        RestartCurLevel();
        AnimPlay.instance.PlayInScene();
        
    }
    IEnumerator _restart()
    {
        yield return new WaitForEndOfFrame();
        EventManager.instance.Send(EventGroup.GAME, (short)GameEvent.RestartGame);
                if(curLevel == LEVEL_NAME.LN_LEVEL8)
        {
            
            List<int> listint = new List<int>();
            listint.Add(1);
            listint.Add(4);
            CommendMgr.instance.curSelectFunc = listint.ToList();
             FuncGrooveItem[] items = FindObjectOfType<MainGameTip>().funcGrooveItems;
            foreach (var item in items)
            {
                if(!item.isUsed && !item.isForbid)
                {
                    item.useGrooveLevelStart("");
                    break;
                }
            }
            
        }


    }
    public void RestartCurLevel()
    {
        player.transform.position = curStruct.StartPos;
        player.GetComponent<Player>().ResetPlayer();
        CommendMgr.instance.playerCommends.Clear();
        CommendMgr.instance.curSelectFunc.Clear();
        if (curLevel != LEVEL_NAME.LN_LEVEL1)
        {
            Dialog.instance.ExecuteBlock((int) curLevel + "-init");
        }
        StartCoroutine(_restart());
        LoadLevel(curLevel);
    }
}

public static class LevelConfig
{
    public static LevelStruct[] levelStructs = new LevelStruct[11]
    {
        new LevelStruct
        {
            StartPos = new Vector2(-12.55f, -4.374939f),
            //leftTime = 2,
            FuncGroove = 4,canDashUni = false,
        },
        new LevelStruct
        {
            StartPos = new Vector2(-12.55f, -4.374939f),
            //leftTime = 2,
            FuncGroove = 4,canDashUni = false,
        },

          new LevelStruct
        {
            StartPos = new Vector2(-10, -0.8742946f),
            //leftTime = 3,
            FuncGroove = 4,
            canDashUni = false,
        },
        new LevelStruct
        {
            StartPos = new Vector2(-12.25f, 0.1257082f),
            //leftTime = 3,
            FuncGroove = 4,
            canDashUni = false,
        },
        new LevelStruct
        {
            StartPos = new Vector2(-16.76f, -1.874295f),
            //leftTime = 3,
            FuncGroove = 1,
            canDashUni = false,
        },
        new LevelStruct
        {
            StartPos = new Vector2(-17.82f, -1.874297f),
            //leftTime = 3,
            FuncGroove = 3,
            canDashUni = false,
        },
                new LevelStruct
        {
            StartPos = new Vector2(0.1f, -0.8742875f),
            //leftTime = 3,
            FuncGroove = 3,
            canDashUni = false,
        },        new LevelStruct
        {
            StartPos = new Vector2(-10.08f, 5.625066f),
            //leftTime = 3,
            FuncGroove = 3,
            canDashUni = false,
        },        new LevelStruct
        {
            StartPos = new Vector2(-10, 0),
            //leftTime = 3,
            FuncGroove = 4,
            canDashUni = false,
        },          new LevelStruct
        {
            StartPos = new Vector2(-15.1f, -5.3f),
            //leftTime = 3,
            FuncGroove = 4,
            canDashUni = false,
        },        new LevelStruct
        {
            StartPos = new Vector2(-6.7f, -5.3f),
            //leftTime = 3,
            FuncGroove = 4,
            canDashUni = false,
        },       
    };

}

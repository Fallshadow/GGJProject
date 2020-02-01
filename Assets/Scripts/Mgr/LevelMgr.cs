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
    }
    public void LoadNectLevel()
    {
        UIMgr.instance.DestoryAllUi();
        if (curLevel == LEVEL_NAME.LN_LEVEL8)
        {
            curLevel = LEVEL_NAME.LN_START;
            curLevel = curLevel + 1;
            curStruct = LevelConfig.levelStructs[(int)curLevel];
            UIMgr.instance.GetUI(PrefabPathConfig.MainGameTip);
            RestartCurLevel();
            SceneManager.LoadScene(1);
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
        SceneManager.LoadScene((int)curLevel);
        //AnimPlay.instance.PlayInScene();
    }
    IEnumerator _restart()
    {
        yield return new WaitForEndOfFrame();
        EventManager.instance.Send(EventGroup.GAME, (short)GameEvent.RestartGame);

    }
    public void RestartCurLevel()
    {
        player.transform.position = curStruct.StartPos;
        player.GetComponent<Player>().ResetPlayer();
        CommendMgr.instance.playerCommends.Clear();
        CommendMgr.instance.curSelectFunc.Clear();
        Dialog.instance.ExecuteBlock((int)curLevel + "-init");
        StartCoroutine(_restart());
    }
}

public static class LevelConfig
{
    public static LevelStruct[] levelStructs = new LevelStruct[9]
    {
        new LevelStruct
        {
            StartPos = new Vector2(-10, 0),
            //leftTime = 2,
            FuncGroove = 4,canDashUni = false,
        },
        new LevelStruct
        {
            StartPos = new Vector2(-10, 0),
            //leftTime = 2,
            FuncGroove = 4,canDashUni = false,
        },
        new LevelStruct
        {
            StartPos = new Vector2(-10, 0),
            //leftTime = 3,
            FuncGroove = 4,
            canDashUni = false,
        },
        new LevelStruct
        {
            StartPos = new Vector2(-10, 0),
            //leftTime = 3,
            FuncGroove = 4,
            canDashUni = false,
        },
        new LevelStruct
        {
            StartPos = new Vector2(-19.6f, 0),
            //leftTime = 3,
            FuncGroove = 4,
            canDashUni = false,
        },
                new LevelStruct
        {
            StartPos = new Vector2(-19.6f, 0),
            //leftTime = 3,
            FuncGroove = 4,
            canDashUni = false,
        },        new LevelStruct
        {
            StartPos = new Vector2(0, 0),
            //leftTime = 3,
            FuncGroove = 4,
            canDashUni = false,
        },        new LevelStruct
        {
            StartPos = new Vector2(-10, 0),
            //leftTime = 3,
            FuncGroove = 4,
            canDashUni = false,
        },        new LevelStruct
        {
            StartPos = new Vector2(-10, 0),
            //leftTime = 3,
            FuncGroove = 4,
            canDashUni = false,
        },
    };

}

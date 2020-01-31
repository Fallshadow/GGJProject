using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum LEVEL_NAME
{
    LN_START = 0,
    LN_LEVEL1,
    LN_LEVEL2,
}
public struct LevelStruct
{
    public Vector2 StartPos;
    public int leftTime;
    public int FuncGroove;
}
public class LevelMgr : SingletonMonoBehaviorNoDestroy<LevelMgr>
{
    public LEVEL_NAME curLevel = LEVEL_NAME.LN_START;
    public LevelStruct curStruct;
    [HideInInspector]
    public GameObject player = null;
    private void Start() {
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
        if(curLevel ==LEVEL_NAME.LN_LEVEL2)
        {
            curLevel = LEVEL_NAME.LN_START;
            SceneManager.LoadScene(0);
            return;
        }
        LEVEL_NAME level = curLevel + 1;
        curLevel = level;
        curStruct = LevelConfig.levelStructs[(int)curLevel];
        if(curLevel != LEVEL_NAME.LN_START)
        {
            UIMgr.instance.GetUI(PrefabPathConfig.MainGameTip);
        }
        RestartCurLevel();
        SceneManager.LoadScene((int)level);

    }

    public void RestartCurLevel()
    {
        player.transform.position = curStruct.StartPos;
        CommendMgr.instance.playerCommends.Clear();
        CommendMgr.instance.curSelectFunc.Clear();
        EventManager.instance.Send(EventGroup.GAME,(short)GameEvent.RestartGame);
    }
}

public static class LevelConfig
{
    public static LevelStruct[] levelStructs = new LevelStruct[3]
    {
        new LevelStruct
        {
            StartPos = new Vector2(-10, 0),
            leftTime = 2,
            FuncGroove = 4,
        },
        new LevelStruct
        {
            StartPos = new Vector2(-10, 0),
            leftTime = 2,
            FuncGroove = 3,
        },
        new LevelStruct
        {
            StartPos = new Vector2(-10, 0),
            leftTime = 3,
            FuncGroove = 4,
        },
    };

}

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
}
public class LevelMgr : SingletonMonoBehaviorNoDestroy<LevelMgr>
{
    public LEVEL_NAME curLevel = LEVEL_NAME.LN_START;
    public LevelStruct curStruct;
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
        LEVEL_NAME level = curLevel + 1;
        SceneManager.LoadScene((int)level);
        UIMgr.instance.DestoryAllUi();
        UIMgr.instance.GetUI(PrefabPathConfig.MainGameTip);
        curLevel = level;
        curStruct = LevelConfig.levelStructs[(int)curLevel];
        RestartCurLevel();
    }

    public void RestartCurLevel()
    {
        player.transform.position = curStruct.StartPos;
    }
}

public static class LevelConfig
{
    public static LevelStruct[] levelStructs = new LevelStruct[3]
    {
        new LevelStruct
        {
            StartPos = new Vector2(-10, 0)
        },
        new LevelStruct
        {
            StartPos = new Vector2(-10, 0)
        },
        new LevelStruct
        {
            StartPos = new Vector2(-10, 0)
        },
    };

}

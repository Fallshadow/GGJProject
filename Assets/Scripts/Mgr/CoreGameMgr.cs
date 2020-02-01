using System;

public class CoreGameMgr : SingletonMonoBehaviorNoDestroy<CoreGameMgr>
{

    private void Start()
    {

    }

    public void Victory()
    {
        if (!Dialog.instance.ExecuteBlock((int)LevelMgr.instance.curLevel + "-" + "end", () => LevelMgr.instance.LoadNectLevel()))
        {
            LevelMgr.instance.LoadNectLevel();
        }
    }

    public void Failed()
    {
        LevelMgr.instance.RestartCurLevel();
    }
}

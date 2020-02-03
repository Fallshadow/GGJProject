using System;

public class CoreGameMgr : SingletonMonoBehaviorNoDestroy<CoreGameMgr>
{
    public bool hadDead = false;
    private void Start()
    {

    }

    public void Victory()
    {

            LevelMgr.instance.LoadNectLevel();

    }

    public void Failed()
    {
        LevelMgr.instance.RestartCurLevel();
    }
}

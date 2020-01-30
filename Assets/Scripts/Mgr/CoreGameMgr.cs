using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoreGameMgr : SingletonMonoBehaviorNoDestroy<CoreGameMgr>
{
    
    private void Start() {
        
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

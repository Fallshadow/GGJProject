using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainGameTip : MonoBehaviour
{

    public void RestartLevel()
    {
        LevelMgr.instance.RestartCurLevel();
    }
}

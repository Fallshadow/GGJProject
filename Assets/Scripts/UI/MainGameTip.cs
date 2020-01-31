using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainGameTip : MonoBehaviour
{
    public Text leftTime = null;
    public void RestartLevel()
    {
        LevelMgr.instance.RestartCurLevel();
    }

    public void Refresh()
    {
        leftTime.text = "剩余可用融合次数： " + LevelMgr.instance.curStruct.leftTime.ToString();
    }

    public void OpenMergeWindow()
    {
        UIMgr.instance.GetUI(PrefabPathConfig.MergeWindow);
    }
}

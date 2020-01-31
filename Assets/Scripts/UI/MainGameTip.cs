using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainGameTip : MonoBehaviour
{
    public FuncGrooveItem[] funcGrooveItems;
    public ControlWindowItem controlWindowItem = null;
    public Text leftTime = null;
    public void RestartLevel()
    {
        LevelMgr.instance.RestartCurLevel();
    }

    public void Refresh()
    {
        // leftTime.text = "剩余可用融合次数： " + LevelMgr.instance.curStruct.leftTime.ToString();
        for (int i = 0; i < funcGrooveItems.Length; i++)
        {
            funcGrooveItems[i].Refresh();
            funcGrooveItems[i].Init(LevelMgr.instance.curStruct.FuncGroove < i + 1);
        }
        controlWindowItem.RefreshShow();
    }

    public void OpenMergeWindow()
    {
        UIMgr.instance.GetUI(PrefabPathConfig.MergeWindow);
    }
private void Start() {
    EventManager.instance.Register(EventGroup.GAME,(short)GameEvent.RestartGame,Refresh);
}
private void OnDestroy() {
    EventManager.instance.Unregister(EventGroup.GAME,(short)GameEvent.RestartGame,Refresh);
}

}

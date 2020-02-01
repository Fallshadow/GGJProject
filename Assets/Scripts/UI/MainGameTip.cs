using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainGameTip : MonoBehaviour
{
    public FuncGrooveItem[] funcGrooveItems;
    public ControlWindowItem controlWindowItem = null;
    public Text leftTime = null;
    public string emotiontext = "";
    
    public void RestartLevel()
    {
        EventManager.instance.Send(EventGroup.GAME,(short)GameEvent.PlayerDie);
    }

    public void Refresh()
    {
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

public void closeTo()
{
    FindObjectOfType<FollowPlayer>().CloseToPlayerPos();
}
public void awayTo()
{
    FindObjectOfType<FollowPlayer>().AwayToPlayerPos();
}
public void emotion()
{
    Debug.Log(CommendMgr.instance.AddEmotionMode(emotiontext));
}

}

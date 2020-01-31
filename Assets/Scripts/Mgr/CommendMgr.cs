using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommendMgr : SingletonMonoBehaviorNoDestroy<CommendMgr>
{
    public List<List<int>> playerCommends = new List<List<int>>();
    public List<int> curSelectFunc = new List<int>();
    private void Start() {
        EventManager.instance.Register<int>(EventGroup.UI,(short)UiEvent.ControlFuncSelect,AddCurSelectFunc);
        EventManager.instance.Register<int>(EventGroup.UI,(short)UiEvent.ControlFuncUnSelect,RemoveCurSelectFunc);
        EventManager.instance.Register(EventGroup.UI,(short)UiEvent.UseGroove,ClearCurSelectFunc);

    }
    private void OnDestroy() {
        EventManager.instance.Unregister<int>(EventGroup.UI,(short)UiEvent.ControlFuncSelect,AddCurSelectFunc);
        EventManager.instance.Unregister<int>(EventGroup.UI,(short)UiEvent.ControlFuncUnSelect,RemoveCurSelectFunc);
        EventManager.instance.Unregister(EventGroup.UI,(short)UiEvent.UseGroove,ClearCurSelectFunc);

    }
    private void AddCurSelectFunc(int index)
    {
        curSelectFunc.Add(index);
    }    private void RemoveCurSelectFunc(int index)
    {
        curSelectFunc.Remove(index);
    } private void ClearCurSelectFunc()
    {
        curSelectFunc.Clear();
    }
    public void AddPlayerCommend(List<int> commends)
    {
        bool containsThis = false;
        int containsThisIndex = 0;
        for (int i = 0; i < playerCommends.Count; i++)
        {
            foreach (var commend in commends)
            {
                if (playerCommends[i].Contains(commend))
                {
                    containsThis = true;
                    containsThisIndex = i;
                    break;
                }
            }
        }

        if (containsThis)
        {
            foreach (var item in commends)
            {
                if (!playerCommends[containsThisIndex].Contains(item))
                    playerCommends[containsThisIndex].Add(item);
            }
        }
        else
        {
            playerCommends.Add(commends.ToList());
            LevelMgr.instance.curStruct.leftTime--;
        }
    }
    public void AddCurPlayerCommend()
    {
        bool containsThis = false;
        int containsThisIndex = 0;
        for (int i = 0; i < playerCommends.Count; i++)
        {
            foreach (var commend in curSelectFunc)
            {
                if (playerCommends[i].Contains(commend))
                {
                    containsThis = true;
                    containsThisIndex = i;
                    break;
                }
            }
        }

        if (containsThis)
        {
            foreach (var item in curSelectFunc)
            {
                if (!playerCommends[containsThisIndex].Contains(item))
                    playerCommends[containsThisIndex].Add(item);
            }
        }
        else
        {
            playerCommends.Add(curSelectFunc.ToList());
            LevelMgr.instance.curStruct.leftTime--;
        }
    }
}

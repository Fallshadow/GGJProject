using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommendMgr : SingletonMonoBehaviorNoDestroy<CommendMgr>
{
    public List<List<int>> playerCommends = new List<List<int>>();
    private void Start()
    {

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
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommendMgr : SingletonMonoBehaviorNoDestroy<CommendMgr>
{
    public List<int[]> playerCommends = new List<int[]>();
    private void Start() {
        playerCommends.Add(new int[]{1,4});
    }
}

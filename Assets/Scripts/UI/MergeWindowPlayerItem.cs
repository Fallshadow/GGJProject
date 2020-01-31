using System.Linq;
using System.Net.Mime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MergeWindowPlayerItem : MonoBehaviour
{
    public MergeWindowPlayerText[] textBtn = null;
    public List<int> curNumber = new List<int>();
    public Text[] funcText = null;
    public void Merge()
    {
        curNumber.Clear();
        foreach (var item in textBtn)
        {   
            if(item.textActive)
            {
                curNumber.Add(item.number);
            }
        }
        CommendMgr.instance.AddPlayerCommend(curNumber);
        ClearText();
        ShowCurFuncs();
    }

    public void ClearText()
    {
        foreach (var item in textBtn)
        {
            item.textActive = false;
            item.SetCommon();
        }
    }

    public void ShowCurFuncs()
    {
        for (int i = 0; i < funcText.Length; i++)
        {
            funcText[i].text = "";
            if(CommendMgr.instance.playerCommends.Count < i + 1)
            {
                return;
            }
            for (int j = 0; j < CommendMgr.instance.playerCommends[i].Count; j++)
            {
                funcText[i].text += CommendMgr.instance.playerCommends[i][j].ToString()+ " ";
            }
            
        }
        
    }
}

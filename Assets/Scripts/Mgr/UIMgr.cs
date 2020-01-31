﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIMgr : SingletonMonoBehaviorNoDestroy<UIMgr>
{
    public Transform MainRoot = null;
    private Dictionary<string,GameObject> dictUI = new Dictionary<string, GameObject>();
    public void DestoryAllUi()
    {
        foreach (Transform item in MainRoot)
        {
            Destroy(item.gameObject);
        }
    }
    public void GetUI(string name)
    {
        GameObject goui = null;
        dictUI.TryGetValue(name,out goui);
        if(goui == null)
        {
            goui = Resources.Load(PrefabPathConfig.PrefabFolder + name) as GameObject;
            dictUI.Add(name,goui);
        }
        Instantiate(goui,MainRoot);
        goui.SetActive(true);
        goui.transform.SetAsLastSibling();
    }

    public Sprite GetFuncBtnSprite(int number)
    {
        string path = PrefabPathConfig.SpriteFolder + PrefabPathConfig.SpriteFuncBtnName + number.ToString();
        Sprite sprite = Resources.Load<Sprite>(path);
        return sprite;
    }
}

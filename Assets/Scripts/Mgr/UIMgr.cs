using System.Collections;
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
            item.gameObject.SetActive(false);
        }
    }
    public void GetUI(string name)
    {
        GameObject goui = null;
        dictUI.TryGetValue(name,out goui);
        if(goui == null)
        {
            goui = Resources.Load(PrefabPathConfig.PrefabFolder + name) as GameObject;
            Instantiate(goui,MainRoot);
            dictUI.Add(name,goui);
        }
        else
        {
        goui = MainRoot.Find(name + "(Clone)").gameObject;

        }
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

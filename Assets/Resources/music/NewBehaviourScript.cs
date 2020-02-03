using System.Net.Mime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class NewBehaviourScript : MonoBehaviour
{
    public GameObject ob = null;
    public RawImage image = null;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }public void aaaPlayJoke()
{
    AudioPlayMgr.instance.PlayJoke(4);
}
public void destoryob()
{
    image.DOFade(0,3);
    UIMgr.instance.DestoryAllUi();
}

}

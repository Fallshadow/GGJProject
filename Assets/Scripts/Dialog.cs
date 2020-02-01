using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;
using UnityEngine.UI;

public class Dialog : MonoBehaviour
{
    private Flowchart _flowchart;
    private GameObject _showImage;
    private GameObject _showText;
    void Awake()
    {
        DontDestroyOnLoad(this);
        _flowchart = GetComponentInChildren<Flowchart>();
        _showImage = transform.Find("ShowPanel").Find("ShowImage").gameObject;
        _showText = transform.Find("ShowPanel").Find("ShowImage").Find("ShowText").gameObject;
    }

    public void ExecuteBlock(string blockName, Action onComplete = null)
    {
        if (_flowchart == null) return;
        var block = _flowchart.FindBlock(blockName);
        if (block == null) return;
        _showImage.SetActive(false);
        _showText.SetActive(false);
        block.Stop();
        _flowchart.ExecuteBlock(block, 0, onComplete);
    }
}

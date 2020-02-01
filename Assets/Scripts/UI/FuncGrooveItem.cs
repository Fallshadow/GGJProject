﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class FuncGrooveItem : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [Header("State")]
    public bool MouseEnter = false;
    public bool isUsed = false;
    public bool isForbid = false;
    [Header("UI")]
    public Image thisImage = null;
    public Image forbidImage = null;
    public Color activeColor;
    public Color neactiveColor;
    void Awake()
    {
        EventManager.instance.Register(EventGroup.UI, (short)UiEvent.CURSORENDFRAG, useGroove);
    }
    private void OnDestroy()
    {
        EventManager.instance.Unregister(EventGroup.UI, (short)UiEvent.CURSORENDFRAG, useGroove);
    }

    public void Init(bool forbid)
    {
        SetActiveColor();
        isForbid = forbid;
        forbidImage.gameObject.SetActive(isForbid);
        isUsed = isForbid;
    }
    private void useGroove()
    {
        if (!MouseEnter || isUsed)
        {
            return;
        }
        string showText = "";
        foreach (var item in CommendMgr.instance.curSelectFunc)
        {    
            switch (item)
            {
                case 0:
                    showText += "←";
                    break;
                case 1:
                    showText += "→";
                    break;
                case 2:
                    showText += "↑";
                    break;
                case 3:
                    showText += "↓";
                    break;
                case 4:
                    showText += "跳";
                    break;
                case 5:
                    showText += "冲";
                    break;
                default:
                    break;
            }
        }
        CommendMgr.instance.AddCurPlayerCommend();
        EventManager.instance.Send(EventGroup.UI,(short)UiEvent.UseGroove);
        isUsed = true;
        SetNeactiveColor();
    }

    public void SetActiveColor()
    {
thisImage.color = activeColor;
    }
    public void SetNeactiveColor()
    {
thisImage.color = neactiveColor;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        MouseEnter = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        MouseEnter = false;
    }

    public void Refresh()
    {
        isUsed = false;
        isForbid = false;
        SetActiveColor();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ControlFuncBtn : MonoBehaviour,IBeginDragHandler,IDragHandler,IEndDragHandler
{
    public Texture2D texture2D;
    public ControlCommand controlCommand = ControlCommand.left;
    private Image icon = null;
    public bool Selected = false;
    public Image SelectedImage = null;
    void Start()
    {
        
    }

    public void Init(ControlCommand controlCommand)
    {
        gameObject.SetActive(true);
        icon = GetComponent<Image>();
        this.controlCommand = controlCommand;
        icon.sprite = UIMgr.instance.GetFuncBtnSprite((int)controlCommand);
    }

    public void Select()
    {
        if(Selected)
        {
            Selected = false;
            SelectedImage.gameObject.SetActive(false);
            EventManager.instance.Send<int>(EventGroup.UI,(short)UiEvent.ControlFuncUnSelect,(int)controlCommand);
        }
        else
        {
            Selected =true;
            SelectedImage.gameObject.SetActive(true);
            EventManager.instance.Send<int>(EventGroup.UI,(short)UiEvent.ControlFuncSelect,(int)controlCommand);
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if(!Selected)
        {
            return;
        }
        Cursor.SetCursor(null,Vector2.zero,CursorMode.Auto);
        EventManager.instance.Send(EventGroup.UI,(short)UiEvent.CURSORENDFRAG);
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if(!Selected)
        {
            return;
        }
        Cursor.SetCursor(texture2D,Vector2.zero,CursorMode.Auto);
    }

    public void OnDrag(PointerEventData eventData)
    {
        if(!Selected)
        {
            return;
        }
        Cursor.SetCursor(texture2D,Vector2.zero,CursorMode.Auto);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class ControlWindowItem : MonoBehaviour
{
    public InputField inputField = null;
    public Transform funcBtnContent = null;
    public GameObject funcTemplate = null;
    public Button openhideBtn = null;
    [Header("Dotween Setting")]
    public Ease ease = Ease.InOutBack;
    public Ease ease2 = Ease.Linear;
    public bool isShowing = false;
    public float showY = 0;
    public float hideY = 0;
    public float duration = 0.7f;
    public GameObject groovewinObject = null;
    public bool alsoFuncGrooveWin = false;

    public float showGrooveWinY = 0;
    public float hideGrooveWinY = 0;
    public GameObject DestoryFunc = null;

    public void Show()
    {

        EventManager.instance.Register(EventGroup.UI,(short)UiEvent.UseGroove,RefreshShow);
        EventManager.instance.Register(EventGroup.UI,(short)UiEvent.AlwaysOpenConwin,AlwaysOpenControlWindow);
        EventManager.instance.Register(EventGroup.UI,(short)UiEvent.CanCloseConwin,CanHideControlWindow);
        if(alsoFuncGrooveWin)
        {
            groovewinObject.transform.DOLocalMoveY(showGrooveWinY,duration).SetEase(ease2);
        }
        transform.DOLocalMoveY(showY,duration).SetEase(ease);
        RefreshShow();
    }
    public void Hide()
    {
        EventManager.instance.Unregister(EventGroup.UI,(short)UiEvent.UseGroove,RefreshShow);
        EventManager.instance.Unregister(EventGroup.UI,(short)UiEvent.AlwaysOpenConwin,AlwaysOpenControlWindow);
        EventManager.instance.Unregister(EventGroup.UI,(short)UiEvent.CanCloseConwin,CanHideControlWindow);
        if(alsoFuncGrooveWin)
        {
            groovewinObject.transform.DOLocalMoveY(hideGrooveWinY,duration).SetEase(ease2);
        }
        transform.DOLocalMoveY(hideY,duration).SetEase(ease);
        CommendMgr.instance.curSelectFunc.Clear();
    }

    public void AlwaysOpenControlWindow()
    {
        openhideBtn.interactable = false;
    }
    public void CanHideControlWindow()
    {
        openhideBtn.interactable = true;
    }
    public void ChangeState()
    {
        //TODO:待加入音效
        if(isShowing)
        {
            isShowing =false;
            Hide();
        }
        else
        {
            isShowing =true;
            Show();
        }
    }


    private void Start() {
        
        RefreshShow();

    }

    public void RefreshShow()
    {
        funcBtnContent = transform.Find("HorizontalFuncBtns");
        for (int i = 0; i < funcBtnContent.childCount; i++)
        {
            Destroy(funcBtnContent.GetChild(i).gameObject);
        }
        foreach (var item in ControlMgr.instance.UnLockFunc)
        {
            if(item == ControlCommand.destroy)
            {
                DestoryFunc.gameObject.SetActive(true);
                continue;
            }
            if(item == ControlCommand.love || item == ControlCommand.evol ||item == ControlCommand.authorize )
            {
                continue;
            }
            GameObject go = Instantiate(funcTemplate,funcBtnContent);
            go.GetComponent<ControlFuncBtn>().Init(item);
        } 
    }


#region BTN
    public void PlaySound()
    {
        //TODO:实现打字音效
    }
    public void InputCommand()
    {
        if(ControlMgr.instance.InputCommand(inputField.text))
        {
            Debug.Log("却有此功能");
            RefreshShow();
        }
        else
        {
            Debug.Log("你这指令不行,或者已经激活了");
        }

    }
    #endregion
}

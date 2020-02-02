using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;
using UnityEngine.UI;

public class Dialog : SingletonMonoBehaviorNoDestroy<Dialog>
{
    public float imageFadeSpeed = 1f;
    private Flowchart _flowchart;
    private Image _showImage;
    private Image _showImageAround;
    private CanvasGroup _showImageCanvasGroup;
    private CanvasGroup _showImageAroundCanvasGroup;
    private Animator _showAnimator;
    private Text _showText;
    private bool _showImageFade;
    private float _targetAlpha;
    private bool _waitInput;
    void Start()
    {
        _showImageFade = false;
        _waitInput = false;
        _targetAlpha = 1f;
        _flowchart = GetComponentInChildren<Flowchart>();
        _showImage = transform.Find("ShowPanel").Find("ShowImage").GetComponent<Image>();
        _showImageAround = transform.Find("ShowPanel").Find("ShowImageAround").GetComponent<Image>();
        _showAnimator = _showImage.GetComponent<Animator>();
        _showImageCanvasGroup = _showImage.GetComponent<CanvasGroup>();
        _showImageAroundCanvasGroup = _showImageAround.GetComponent<CanvasGroup>();
        _showImageCanvasGroup.alpha = 0f;
        _showText = transform.Find("ShowPanel").Find("ShowImage").Find("ShowText").GetComponent<Text>();
    }

    public bool ExecuteBlock(string blockName, Action onComplete = null)
    {
        var block = _flowchart.FindBlock(blockName);
        if (block == null) return false;
        block.Stop();
        return _flowchart.ExecuteBlock(block, 0, onComplete);
    }

    public void ShowImage(Sprite sprite, bool fade = true, string message = null)
    {
        if (sprite != null)
        {
            _showAnimator.enabled = false;
            _showImage.sprite = sprite;
            _showImage.SetNativeSize();
            if (fade)
            {
                _showImageCanvasGroup.alpha = 0f;
                _showImageFade = true;
                _targetAlpha = 1f;
            }
            else
            {
                _showImageCanvasGroup.alpha = 1f;
            }
        }
        if (message != null)
        {
            _showText.text = message;
        }
    }

    public void ShowImageAround(Sprite sprite)
    {
        if (sprite != null)
        {
            _showImageAround.sprite = sprite;
            _showImageAround.SetNativeSize();
            _showImageAroundCanvasGroup.alpha = 1f;
        }
    }

    public void HideImageAround()
    {
        _showImageAroundCanvasGroup.alpha = 0f;
    }

    public void HideImage(bool fade = true)
    {
        if (fade)
        {
            _showImageCanvasGroup.alpha = 1f;
            _showImageFade = true;
            _targetAlpha = 0f;
        }
        else
        {
            _showImageCanvasGroup.alpha = 0f;
        }
    }

    public void ShowControlWindow()
    {
        ControlWindowItem controlWindow = FindObjectOfType(typeof(ControlWindowItem)) as ControlWindowItem;
        if (controlWindow != null)
        {
            controlWindow.Show();
        }
    }

    public void HideControlWindow()
    {
        ControlWindowItem controlWindow = FindObjectOfType(typeof(ControlWindowItem)) as ControlWindowItem;
        if (controlWindow != null)
        {
            controlWindow.Hide();
        }
    }

    public void WaitInput()
    {
        _waitInput = true;
    }

    public void LockControlWindow()
    {
        ControlWindowItem controlWindow = FindObjectOfType(typeof(ControlWindowItem)) as ControlWindowItem;
        if (controlWindow != null)
        {
            controlWindow.AlwaysOpenControlWindow();
        }
    }

    public void UnLockControlWindow()
    {
        ControlWindowItem controlWindow = FindObjectOfType(typeof(ControlWindowItem)) as ControlWindowItem;
        if (controlWindow != null)
        {
            controlWindow.CanHideControlWindow();
        }
    }


    public void PlayAnimation(string stateName)
    {
        _showAnimator.enabled = true;
        _showAnimator.Play(stateName);
    }

    public void JudgeGoodOrBad()
    {
        _flowchart.SetBooleanVariable("goodEnd",CommendMgr.instance.JudgeGoodOrBad());
    }

    public void JudgeHasDead()
    {
        _flowchart.SetBooleanVariable("hasDead", CoreGameMgr.instance.hadDead);
    }
    public void AnimAwake()
    {
        FindObjectOfType<AnimScript>().SetAwakeParam()
    }

    void Update()
    {
        if (_showImageFade)
        {
            _showImageCanvasGroup.alpha = Mathf.MoveTowards(_showImageCanvasGroup.alpha, _targetAlpha, imageFadeSpeed * Time.deltaTime);
            if (_showImageCanvasGroup.alpha == _targetAlpha)
            {
                _showImageFade = false;
            }
        }

        if (_waitInput)
        {
            ControlWindowItem controlWindow = FindObjectOfType(typeof(ControlWindowItem)) as ControlWindowItem;
            if (controlWindow != null)
            {
                InputField input = controlWindow.GetComponentInChildren<InputField>();
                if (input != null)
                {
                    string blockName = (int)LevelMgr.instance.curLevel + "-" + input.text;
                    if (_flowchart.HasBlock(blockName))
                    {
                        _flowchart.ExecuteBlock(blockName);
                        input.text = ""; 
                        controlWindow.Hide();
                        _waitInput = false;
                    }
                }
            }
        }
    }
}

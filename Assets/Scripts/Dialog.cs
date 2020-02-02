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
    private CanvasGroup _showImageCanvasGroup;
    private Animation _showAnimation;
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
        _showAnimation = transform.Find("ShowPanel").Find("ShowImage").GetComponent<Animation>();
        _showImageCanvasGroup = _showImage.GetComponent<CanvasGroup>();
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
        EventManager.instance.Send(EventGroup.UI, (short)UiEvent.AlwaysOpenConwin);
    }

    public void UnLockControlWindow()
    {
        EventManager.instance.Send(EventGroup.UI, (short)UiEvent.CanCloseConwin);
    }


    public void PlayAnimation(AnimationClip clip)
    {
        _showAnimation.Stop();
        _showAnimation.AddClip(clip, clip.name);
        _showAnimation.clip = clip;
        _showAnimation.Play(clip.name);
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

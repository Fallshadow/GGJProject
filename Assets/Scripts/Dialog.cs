using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;
using UnityEngine.UI;

public class Dialog : MonoBehaviour
{
    public float imageFadeSpeed = 1f;
    private Flowchart _flowchart;
    private Image _showImage;
    private CanvasGroup _showImageCanvasGroup;
    private Text _showText;
    private bool _showImageFade;
    private float _targetAlpha;
    void Awake()
    {
        DontDestroyOnLoad(this);
        _showImageFade = false;
        _targetAlpha = 1f;
        _flowchart = GetComponentInChildren<Flowchart>();
        _showImage = transform.Find("ShowPanel").Find("ShowImage").GetComponent<Image>();
        _showImageCanvasGroup = _showImage.GetComponent<CanvasGroup>();
        _showImageCanvasGroup.alpha = 0f;
        _showText = transform.Find("ShowPanel").Find("ShowImage").Find("ShowText").GetComponent<Text>();
    }

    public void ExecuteBlock(string blockName, Action onComplete = null)
    {
        var block = _flowchart.FindBlock(blockName);
        if (block == null) return;
        block.Stop();
        _flowchart.ExecuteBlock(block, 0, onComplete);
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
    }
}

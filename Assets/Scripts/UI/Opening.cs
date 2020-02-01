using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Opening : MonoBehaviour
{
    public enum ShowMode
    {
        FadeIn,
        Typewriter
    }
    public string showStatement;
    public float showStartTime = 1f;
    public float showSpeed = 0.5f;
    public ShowMode showMode = ShowMode.Typewriter;
    public AudioClip soundEffect;
    public float showDurationTime = 2f;
    public float fadeSpeed = 0.4f;
    private CanvasGroup _canvasGroup;
    private AudioSource _audioSource;
    private Text _text;
    private CanvasGroup _textCanvasGroup;
    private float _textSubLength;
    private float _textAlpha;
    private bool _showFinished;
    private bool _keyDown;

    void Awake()
    {
        _canvasGroup = GetComponent<CanvasGroup>();
        _audioSource = GetComponent<AudioSource>();
        _text = GetComponentInChildren<Text>();
        _textSubLength = 0;
        _textCanvasGroup = _text.GetComponent<CanvasGroup>();
        _textCanvasGroup.alpha = _textAlpha = 0f;
        _showFinished = false;
        _keyDown = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (_showFinished && (Input.anyKeyDown|| _keyDown))
        {
            _keyDown = true;
            var delta = fadeSpeed * Time.deltaTime;
            var alpha = Mathf.MoveTowards(_canvasGroup.alpha, 0, delta);
            _canvasGroup.alpha = alpha;
            if (alpha <= 0)
            {
                gameObject.SetActive(false);
            }
        }
        else
        {
            if (Time.time > showStartTime)
            {

                switch (showMode)
                {
                    case ShowMode.FadeIn:
                        {
                            if (soundEffect != null && !_audioSource.isPlaying)
                            {
                                _audioSource.clip = soundEffect;
                                _audioSource.pitch = soundEffect.length / (1f / showSpeed);
                                _audioSource.Play();
                            }
                            _text.text = showStatement;
                            if (_textAlpha < 1f)
                            {
                                var textDelta = showSpeed * Time.deltaTime;
                                _textAlpha = Mathf.MoveTowards(_textCanvasGroup.alpha, 1f, textDelta);
                                _textCanvasGroup.alpha = _textAlpha;
                                if (_textAlpha >= 1f)
                                {
                                    _audioSource.Stop();
                                    _showFinished = true;
                                }
                            }
                        }
                        break;
                    case ShowMode.Typewriter:
                        {
                            if (soundEffect != null && !_audioSource.isPlaying)
                            {
                                _audioSource.clip = soundEffect;
                                _audioSource.pitch = soundEffect.length / (showStatement.Length / (showSpeed * 10f));
                                _audioSource.Play();
                            }
                            _textCanvasGroup.alpha = 1f;
                            if (_textSubLength < showStatement.Length)
                            {
                                _textSubLength += showSpeed * Time.deltaTime * 10f;
                                _text.text = showStatement.Substring(0, (int)_textSubLength);
                                if (_textSubLength >= showStatement.Length)
                                {
                                    _audioSource.Stop();
                                    _showFinished = true;
                                }
                            }
                        }
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }
    }
}

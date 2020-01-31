using System.Security.Cryptography.X509Certificates;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MergeWindowPlayerText : MonoBehaviour
{
    public Text text = null;
    public Color commonColor;
    public Color activeColor;
    public Color neactiveColor;
    public int number = 0;
    public bool textActive = false;
    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake()
    {
        text = GetComponent<Text>();
    }
    public void SetCommon()
    {
        text.color = commonColor;
    }
    public void SetActive()
    {
        text.color = activeColor;
    }
    public void SetNeactive()
    {
        text.color = neactiveColor;
    }
    public void Click()
    {
        if(textActive)
        {
            SetCommon();
            textActive = false;
        }
        else
        {
            SetActive();
            textActive = true;
        }
    }
}

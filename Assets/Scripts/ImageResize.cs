using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageResize : MonoBehaviour
{
    private Image _image;
    // Start is called before the first frame update
    void Awake()
    {
        _image = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_image != null)
        {
            _image.SetNativeSize();
        }
    }
}

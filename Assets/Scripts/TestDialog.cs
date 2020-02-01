using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestDialog : MonoBehaviour
{
    public string blockName;
    private Dialog _dialog;

    void Awake()
    {
        _dialog = GetComponent<Dialog>();
    }

    void Update()
    {
        if (_dialog != null)
        {
            _dialog.ExecuteBlock(blockName);
            blockName = "";
        }
    }
}

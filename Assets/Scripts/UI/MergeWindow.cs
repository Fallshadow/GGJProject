using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MergeWindow : MonoBehaviour
{
    public MergeWindowPlayerItem mergeWindowPlayerItem = null;
    public void Hide()
    {
        mergeWindowPlayerItem.ClearText();
        gameObject.SetActive(false);
    }
}

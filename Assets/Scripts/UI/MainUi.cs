using System.Runtime.InteropServices;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainUi : MonoBehaviour
{
public void StartGame()
{
    LevelMgr.instance.LoadNectLevel();
}
}

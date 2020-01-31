using System.Reflection;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum ControlCommand
{
    left = 0,
    right,
    up,
    down,
    jump,
    dash,
    destory,
    love,
    evol,
}
public class ControlMgr : SingletonMonoBehaviorNoDestroy<ControlMgr>
{
    public List<ControlCommand> UnLockFunc = new List<ControlCommand>();
    private List<string> UnLockFuncName = new List<string>();
    private void Start() {
        var fields = typeof(ControlCommand).GetFields(BindingFlags.Static | BindingFlags.Public);

        foreach (var fi in fields)UnLockFuncName.Add(fi.Name);
    }
    public bool InputCommand(string text)
    {
    
        if(!UnLockFuncName.Contains(text))
        {
            return false;
        }
        ControlCommand controlCommand = (ControlCommand)Enum.Parse(typeof(ControlCommand),text);
        if(UnLockFunc.Contains(controlCommand))
        {
            return false;
        }
        UnLockFunc.Add(controlCommand);

        if(controlCommand == ControlCommand.love)
        {

        }
        if(controlCommand == ControlCommand.evol)
        {
            
        }
        return true;
    }
}

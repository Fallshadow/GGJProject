using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gear : MonoBehaviour
{
    [Tooltip("是否已经触发")]
    [SerializeField]
    protected bool triggered = false;
    /// <summary>
    /// 是否已经触发
    /// </summary>
    public virtual bool Triggered { get { return triggered; } }

    public virtual void Trigger()
    {
        if (!triggered)
        {
            triggered = true;
        }
    }

    protected virtual void OnReStart()
    {

    }

    public virtual void ReStart()
    {
        OnReStart();
        triggered = false;
    }
}

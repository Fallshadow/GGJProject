﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GearSwitch : MonoBehaviour
{
    [Tooltip("绑定的机关列表")]
    [SerializeField] protected List<Gear> gears;
    [Tooltip("是否已经触发")]
    [SerializeField]
    protected bool triggered = false;
    /// <summary>
    /// 绑定的机关列表
    /// </summary>
    public virtual List<Gear> Gears { get { return gears; } }
    /// <summary>
    /// 是否已经触发
    /// </summary>
    public virtual bool Triggered { get { return triggered; } }

    public void Trigger()
    {
        if (!triggered)
        {
            for (var i = 0; i < gears.Count; ++i)
            {
                var gear = gears[i];
                if (gear != null)
                {
                    gear.Trigger();
                }
            }
            triggered = true;
        }
    }
}

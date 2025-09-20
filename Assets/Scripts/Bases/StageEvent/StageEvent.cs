using System;
using Sirenix.OdinInspector;
using UnityEngine;

[Serializable]
public class StageEvent
{
    /// <summary> 關卡時間 </summary>
    [HorizontalGroup(Width = 45)]
    [HideLabel]
    public int time;

    /// <summary> 關卡指令 </summary>
    [SerializeReference]
    [HorizontalGroup]
    [HideLabel]
    [InlineProperty]
    [HideReferenceObjectPicker]
    public StageCommandBase command;

    public StageEvent Clone()
    {
        StageEvent c = new StageEvent();
        c.time = this.time;
        return c;
    }
}

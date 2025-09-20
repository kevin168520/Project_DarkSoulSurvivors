using System;
using UnityEngine;

public enum StageEventType
{
    SpawnEnemy,
    WinStage,
    StartStage
}

[Serializable]
public class StageEvent
{
    [SerializeReference] public StageCommandBase command;
    public StageEventType type = StageEventType.SpawnEnemy;
    [Range(0, 1800)] public float time;     // 出現時間

    public StageEvent Clone()
    {
        StageEvent c = new StageEvent();
        c.time = this.time;
        return c;
    }
}

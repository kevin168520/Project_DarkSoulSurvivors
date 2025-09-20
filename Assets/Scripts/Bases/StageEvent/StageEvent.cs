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
    public string message;                  // 後台訊息

    public StageEvent Clone()
    {
        StageEvent c = new StageEvent();
        c.time = this.time;
        c.message = this.message;
        return c;
    }
}

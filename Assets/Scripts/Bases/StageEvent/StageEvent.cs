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
    public StageEventType type = StageEventType.SpawnEnemy;
    [Range(0, 1800)] public float time;     // 出現時間
    public string message;                  // 後台訊息
    public EnemyScriptable enemy;           // 敵人數據
    [Range(0, 200)] public int enemyCount;  // 敵人數量

    public StageEvent Clone()
    {
        StageEvent c = new StageEvent();
        c.time = this.time;
        c.message = this.message;
        c.enemy = this.enemy;
        c.enemyCount = this.enemyCount;
        return c;
    }
}

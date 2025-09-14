using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum StageEventType
{
    SpawnEnemy,
    WinStage,
    StartStage
}

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/StageEvent", order = 1)]
public class StageEventScriptable : ScriptableObject, IEnumerable<StageEvent>
{
    [SerializeField] private List<StageEvent> table; // 敵人波
    [NonSerialized] private int index = 0;

    /// <summary> 判定事件結束 </summary>
    public bool IsEnd => index > table.Count;

    /// <summary> 事件索引 </summary>
    public int Index => index;

    /// <summary> 下個事件觸發 </summary>
    public bool Check(float time)
    {
        if (IsEnd) return false;
        return table[index].time > time;
    }

    /// <summary> 進下一個 </summary>
    public void Next() => index++;

    /// <summary> 當前事件 </summary>
    public StageEvent Current() => table[index];

    // 用於 foreach 循序
    public IEnumerator<StageEvent> GetEnumerator() => table.GetEnumerator();
    IEnumerator IEnumerable.GetEnumerator() => table.GetEnumerator();
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

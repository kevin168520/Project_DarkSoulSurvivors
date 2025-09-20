using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/StageEvent", order = 1)]
public class StageEventScriptable : ScriptableObject, IEnumerable<StageEvent>
{
    [Title("[時間] [指令]")]
    [SerializeField]
    [ListDrawerSettings(NumberOfItemsPerPage = 10)]
    private List<StageEvent> table; // 敵人波
    [NonSerialized] private int index = 0;

    /// <summary> 判定事件結束 </summary>
    public bool IsEnd => index >= table.Count;

    /// <summary> 事件索引 </summary>
    public int Index => index;

    /// <summary> 下個事件觸發 </summary>
    public bool Check(float time)
    {
        if (IsEnd) return false;
        return time >= table[index].time;
    }

    /// <summary> 進下一個 </summary>
    public void Next() => index++;

    /// <summary> 當前事件 </summary>
    public StageEvent Current() => table[index];

    // 用於 foreach 循序
    public IEnumerator<StageEvent> GetEnumerator() => table.GetEnumerator();
    IEnumerator IEnumerable.GetEnumerator() => table.GetEnumerator();
}

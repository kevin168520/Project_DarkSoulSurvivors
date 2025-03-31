using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 儲存靜態物件管理者 使用 PoolGlobalManager.Instance 進行呼叫
/// </summary>
public class InstanceGlobalManager : GlobalMonoBase<InstanceGlobalManager>
{
    // 物件池
    private InstanceRegistry<MonoBehaviour> registry = new InstanceRegistry<MonoBehaviour>();
    
    
    /// <summary> 添加註冊 Manager </summary>
    public bool Register(MonoBehaviour obj) => registry.Register(obj);
    
    /// <summary> 取得註冊 Manager </summary>
    public T Get<T>() where T : MonoBehaviour => registry.Get<T>();
    // public static bool Get<T>(out T obj) where T : MonoBehaviour => 
    // (Instance.registry.Get(typeof(T), out MonoBehaviour baseObj) 
    // && (obj = baseObj as T) != null) 
    // || ((obj = null) == null); // 保留 用於技術參考

    /// <summary> 添加註冊 Manager </summary>
    public bool Unregister(MonoBehaviour obj) => registry.Unregister(obj);

#if UNITY_EDITOR // 編輯器中檢測註冊 Manager
    [Header("透過開關讀取場景註冊的 manager")]
    [SerializeField] private bool check;
    [SerializeField] private List<MonoBehaviour> instances = new List<MonoBehaviour>();
    void OnValidate() {
        instances.Clear();
        foreach (var kvp in registry.pool) instances.Add(kvp.Value);
    }
#endif
}

using System;
using System.Collections.Generic;
using System.Reflection;

/// <summary>
/// 已知問題，不要使用繼承事件，否則呼叫時重複呼叫<br/>
/// SubMyEvent : MyEvent (同時 SubMyEvent 跟 MyEvent 無法識別)
/// </summary>
public class EventHandler
{
    private readonly MethodInfo invokeEventMethod;

    /// <summary>這個字典用來儲存事件的目前所有。<br/>EventWatcher中會反射參考這個欄位。</summary>
    private readonly Dictionary<Type, List<object>> handlers = new();

    public EventHandler()
    {
        invokeEventMethod = GetType().GetMethod(nameof(InvokeEvent), BindingFlags.Public | BindingFlags.Instance);
    }

    /// <summary> 註冊一個事件 不檢測型別僅內部使用 </summary>
    private void RegisterEvent(Type type, object Event)
    {
        if (!handlers.ContainsKey(type))
            handlers.Add(type, new List<object>());
        handlers[type].Add(Event);
    }

    /// <summary> 註冊一個物件上實作的所有事件 </summary>
    public void RegisterEvent(object Event)
    {
        foreach (var eventInterface in Event.GetType().GetInterfaces())
        {
            if (!eventInterface.IsGenericType || eventInterface.GetGenericTypeDefinition() != typeof(IEvent<>)) continue;
            RegisterEvent(eventInterface.GetGenericArguments()[0], Event);
        }
    }

    /// <summary> 註冊一個事件 </summary>
    public void RegisterEvent<T>(IEvent<T> Event)
    {
        RegisterEvent(typeof(T), Event);
    }

    /// <summary> 呼叫所有標記了T的事件，包括其實作的介面也會被呼叫 </summary>
    public void InvokeEvent<T>(T parameters = default)
    {
        var eventsPairs = new List<KeyValuePair<Type, List<object>>>(handlers);
        foreach (var (type, events) in eventsPairs)
        {
            if(!type.IsAssignableFrom(typeof(T))) continue;

            foreach (object e in events.ToArray())
                if (e is IEvent<T> Event) Event.Execute(parameters);
        }
    }

    /// <summary> 移除指定的事件 </summary>
    /// <param name="Event">繼承IEvent的物件</param>
    public void DeregisterEvent(object Event)
    {
        foreach (var (_, value) in handlers)
            if (value.Contains(Event))
                value.Remove(Event);
    }

    /// <summary> 移除所有 T 的事件 </summary>
    public void DeregisterEvent<T>() => handlers.Remove(typeof(T));

    /// <summary> 移除所有的事件 </summary>
    public void DeregisterAll() => handlers.Clear();
}
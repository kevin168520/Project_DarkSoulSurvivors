public class EventGlobalManager : GlobalMonoBase<EventGlobalManager>
{
    private readonly EventHandler eventHandlers = new();

    /// <summary> 註冊一個實作了Event的物件 </summary>
    public void RegisterEvent<T>(IEvent<T> Event) => eventHandlers.RegisterEvent(Event);

    /// <summary> 註冊一個物件，遍歷其實作的介面，並註冊所有為IEvent的事件 </summary>
    public void RegisterEvent(object Event) => eventHandlers.RegisterEvent(Event);

    /// <summary> 呼叫所有標記了T的事件，包括其實作的介面也會被呼叫 </summary>
    public void InvokeEvent<T>(T parameters = default) => eventHandlers.InvokeEvent(parameters);

    /// <summary> 移除指定的事件 </summary>
    public void DeregisterEvent(object Event) => eventHandlers.DeregisterEvent(Event);

    /// <summary> 移除所有 T 的事件 </summary>
    public void DeregisterEvent<T>() => eventHandlers.DeregisterEvent<T>();

    /// <summary> 移除所有的事件 </summary>
    public void DeregisterAll() => eventHandlers.DeregisterAll();
}

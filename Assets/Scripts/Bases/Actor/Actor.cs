/// <summary>
/// 管理單位數值的容器<br/>
/// Actor[id] 透過此方式最快存取數值<br/>
/// Get&lt;T&gt;() 轉型直接使用泛型取值<br/>
/// Try***() 安全判定請使用此系列
/// </summary>
public class Actor
{
    System.Collections.Generic.Dictionary<int, IActorAttribute> attributes = new();

    public bool Contains(int id) => attributes.ContainsKey(id);
    public void Set(IActorAttribute attr) => attributes[attr.Id] = attr;
    public void Add(IActorAttribute attr) => attributes.Add(attr.Id, attr);
    public IActorAttribute Get(int id) => attributes[id];
    public T Get<T>() where T : ActorAttribute<T>, new() => (T)attributes[ActorAttribute<T>.ID];
    public bool TryGet(int id, out IActorAttribute attr) => attributes.TryGetValue(id, out attr);
    public bool TryGet<T>(out T attr) where T : ActorAttribute<T>, new()
    {
        if (attributes.TryGetValue(ActorAttribute<T>.ID, out var temp))
        {
            attr = (T)temp;
            return true;
        }
        attr = null;
        return false;
    }

    public float this[int id]
    {
        get => attributes[id].Value;
        set => attributes[id].Value = value;
    }
}

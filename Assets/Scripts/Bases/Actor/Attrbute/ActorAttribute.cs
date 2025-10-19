/// <summary>
/// 實作單位數值邏輯<br/>
/// 將 Id 包裝成 Attribute.ID 不需實例化後取的<br/>
/// </summary>
public abstract class ActorAttribute<T> : IActorAttribute
    where T : ActorAttribute<T>, new()
{
    public static readonly int ID = new T().Id;
    public static T Create() => new();
    public static T Create(float value)
    {
        var t = new T();
        t.Value = t.OrigValue = value;
        return t;
    }

    protected abstract int Id { get; }
    public abstract float OrigValue { get; protected set; }
    float IActorAttribute.OrigValue => OrigValue;
    public float Value { get; set; }

    // 效能問題，子類別請勿建立建構式
    // public ActorAttribute() {}

    public void Reset() => Value = OrigValue;

    int IActorAttribute.Id => Id;

    public static implicit operator float(ActorAttribute<T> a) => a.Value;
}
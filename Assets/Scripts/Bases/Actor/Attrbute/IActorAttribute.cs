/// <summary> 定義單位數值接口 </summary>
public interface IActorAttribute
{
    /// <summary> 應所有數值的唯一值 </summary>
    public int Id { get; }
    /// <summary> 開始遊戲時單位數值最初的值 </summary>
    public float OrigValue { get; }
    /// <summary> 經過任何計算後當前的值 </summary>
    public float Value { get; set; }
    public void Reset();
}

public class HpAttribute : ActorAttribute<HpAttribute>
{
    protected override int Id { get => 100; }
    public override float OrigValue { get; protected set; } = 100;
}

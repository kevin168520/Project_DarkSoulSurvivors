public class GoldAttribute : ActorAttribute<GoldAttribute>
{
    protected override int Id { get => 130; }
    public override float OrigValue { get; protected set; } = 0;
}

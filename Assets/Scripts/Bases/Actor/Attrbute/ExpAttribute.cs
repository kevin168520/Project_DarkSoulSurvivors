public class ExpAttribute : ActorAttribute<ExpAttribute>
{
    protected override int Id { get => 120; }
    public override float OrigValue { get; protected set; } = 0;
}

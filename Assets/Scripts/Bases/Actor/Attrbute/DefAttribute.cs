public class DefAttribute : ActorAttribute<DefAttribute>
{
    protected override int Id { get => 140; }
    public override float OrigValue { get; protected set; } = 0;
}

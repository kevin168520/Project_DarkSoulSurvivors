public class AttAttribute : ActorAttribute<AttAttribute>
{
    protected override int Id { get => 150; }
    public override float OrigValue { get; protected set; } = 0;
}

public class MoveAttribute : ActorAttribute<MoveAttribute>
{
    protected override int Id { get => 170; }
    public override float OrigValue { get; protected set; } = 1;
}

public class LevelAttribute : ActorAttribute<LevelAttribute>
{
    protected override int Id { get => 110; }
    public override float OrigValue { get; protected set; } = 1;
}

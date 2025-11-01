public class SpeedAttribute : ActorAttribute<SpeedAttribute>
{
    protected override int Id { get => 160; }
    public override float OrigValue { get; protected set; } = 1;
}

namespace ArchSample.Domain.Core
{
    /// <summary>
    /// TODO: constraints
    /// </summary>
    public record TypedId<T>(T Value) where T : struct;
}

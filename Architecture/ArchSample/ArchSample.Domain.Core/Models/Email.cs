namespace ArchSample.Domain.Core
{
    public record Email
    {
        public string RawValue { get; }

        public Email(string rawValue)
        {
            //TODO: check email via regexp
            RawValue = rawValue;
        }
    }

    /// <summary>
    /// TODO: constraints
    /// </summary>
    public record TypedId<T>(T Value) where T : struct;
}

namespace ArchSample.Domain.Core
{
    public record Phone
    {
        public string RawValue { get; }

        public Phone(string rawValue)
        {
            //TODO: check email via regexp
            RawValue = rawValue;
        }
    }
}

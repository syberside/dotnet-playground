using ArchSample.Domain.Core;
using System;

namespace ArchSample.Domain.User
{
    public record UserId : TypedId<Guid>
    {
        public UserId(Guid Value) : base(Value)
        {
        }
    }
}

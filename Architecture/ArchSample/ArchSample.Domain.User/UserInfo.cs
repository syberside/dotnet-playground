using ArchSample.Domain.Core;
using System;

namespace ArchSample.Domain.User
{
    public record UserInfo(Email Email, string Username, DateTime RegisteredAt);
}

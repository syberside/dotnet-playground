using ArchSample.Domain.Core;
using System;

namespace ArchSample.Domain.User
{
    public interface IUserService
    {
        UserId RegisterUser(Email email, string username);

        UserInfo[] GetUsers(DateTime registeredAfter);

        bool CanUserLoggin(UserId id);
    }
}

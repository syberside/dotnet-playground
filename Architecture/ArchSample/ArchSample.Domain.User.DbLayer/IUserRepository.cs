using System;

namespace ArchSample.Domain.User.DataLayer
{
    public interface IUserRepository
    {
        DbUser GetById(Guid id);
        void Add(DbUser user);
        UserInfo[] GetRegisteredAfter(DateTime registeredAfter);
    }
}

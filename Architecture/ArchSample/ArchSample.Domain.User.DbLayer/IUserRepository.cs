using ArchSample.Domain.Core;
using System;

namespace ArchSample.Domain.User.DataLayer
{
    public interface IUserRepository
    {
        DbUser GetById(Guid id);

        DbUser GetByPhone(Phone phone);
        void Add(DbUser user);
        UserInfo[] GetRegisteredAfter(DateTime registeredAfter);
    }
}

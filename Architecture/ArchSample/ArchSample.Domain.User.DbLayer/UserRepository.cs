using System;

namespace ArchSample.Domain.User.DataLayer
{
    public class UserRepository : IUserRepository
    {
        public void Add(DbUser user)
        {
            //TODO: return using ORM
            throw new NotImplementedException();
        }

        public DbUser GetById(Guid id)
        {
            //TODO: add to ORM
            throw new NotImplementedException();
        }

        public UserInfo[] GetRegisteredAfter(DateTime registeredAfter)
        {
            //TODO: case optimized call (ORM/stored procedure)
            throw new NotImplementedException();
        }
    }
}

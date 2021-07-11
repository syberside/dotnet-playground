using System;

namespace ArchSample.Domain.User.DataLayer
{
    public class UserLogginLogsRepository : IUserLogginLogsRepository
    {
        public DbUserLoggedInOccurance[] GetLogginLogs(DateTime from, DateTime to)
        {
            //TODO: case optimized call (ORM/stored procedure)
            throw new NotImplementedException();
        }
    }
}

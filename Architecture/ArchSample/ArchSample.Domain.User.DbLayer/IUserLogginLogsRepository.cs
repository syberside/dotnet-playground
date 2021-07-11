using System;

namespace ArchSample.Domain.User.DataLayer
{
    public interface IUserLogginLogsRepository
    {
        DbUserLoggedInOccurance[] GetLogginLogs(DateTime from, DateTime to);
    }
}

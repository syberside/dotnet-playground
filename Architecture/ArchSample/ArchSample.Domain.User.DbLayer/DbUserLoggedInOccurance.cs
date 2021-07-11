using System;

namespace ArchSample.Domain.User.DataLayer
{
    /// <summary>
    /// NOTE: This db entity is not a part of aggregation root because it cant be safety loaded via eagier/lazy loading.
    /// </summary>
    public class DbUserLoggedInOccurance
    {
        public Guid Id { get; set; }

        public Guid UserId { get; set; }

        public DateTime At { get; set; }

        public string UserAgentString { get; set; }

        public string Location { get; set; }
    }
}

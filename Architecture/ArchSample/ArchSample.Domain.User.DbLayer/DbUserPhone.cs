using System;

namespace ArchSample.Domain.User.DataLayer
{
    public class DbUserPhone
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string Phone { get; set; }
    }
}

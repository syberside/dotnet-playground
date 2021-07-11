using System;

namespace ArchSample.Domain.User.DataLayer
{
    public class DbUser
    {
        public Guid Id { get; set; }

        public string Email { get; set; }

        public string Username { get; set; }

        public DateTime RegistrationDate { get; set; }

        public bool IsActive { get; set; }
    }
}

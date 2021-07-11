using ArchSample.Domain.Core;
using ArchSample.Domain.User.DataLayer;
using System;

namespace ArchSample.Domain.User.Impl
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _repository;

        public UserService(IUserRepository repository)
        {
            _repository = repository;
        }

        public UserId RegisterUser(Email email, string username)
        {
            var id = new UserId(Guid.NewGuid());
            var user = new DbUser
            {
                Id = id.Value,
                Email = email.RawValue,
                Username = username,
                IsActive = true,
                RegistrationDate = DateTime.Now,
            };
            _repository.Add(user);
            return id;
        }

        public UserInfo[] GetUsers(DateTime registeredAfter) => _repository.GetRegisteredAfter(registeredAfter);
    }
}

using ArchSample.Domain.Core;
using ArchSample.Domain.User.DataLayer;
using System;
using System.Linq;

namespace ArchSample.Domain.User.Impl
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IUserLogginLogsRepository _userLogginLogsRepository;

        public UserService(IUserRepository repository, IUserLogginLogsRepository userLogginLogsRepository)
        {
            _userRepository = repository;
            _userLogginLogsRepository = userLogginLogsRepository;
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
            _userRepository.Add(user);
            return id;
        }

        public UserInfo[] GetUsers(DateTime registeredAfter) => _userRepository.GetRegisteredAfter(registeredAfter);

        public bool CanUserLoggin(UserId id)
        {
            var logs = _userLogginLogsRepository.GetLogginLogs(DateTime.Now.AddDays(1), DateTime.Now);
            //Pseudo business logic
            if (logs.Length > 10 && logs.Count(x => x.UserAgentString.Contains("blabla")) > 5)
            {
                return false;
            }
            return true;
        }
    }
}

using ArchSample.Domain.Core;
using ArchSample.UseCases.Infrastructure;
using ArchSample.UseCases.UserManagement.Commands;

namespace ArchSample.WebAPI
{
    public class UserController
    {
        private readonly ICommandBus _commandBus;

        public UserController(ICommandBus commandBus)
        {
            _commandBus = commandBus;
        }

        /// <summary>
        /// TODO: Add deserializer string -> email
        /// </summary>
        public RegistrationResult Register(Email email, string username)
        {
            //TODO: validation
            var command = new RegisterUserCommand(email, username, UseCases.UserManagement.RegistrationSource.Site);
            var result = _commandBus.Execute<RegisterUserCommand, RegistrationResult>(command);
            return result;
        }
    }
}

using ArchSample.Domain.Core;
using ArchSample.Domain.Notifications;
using ArchSample.Domain.User;
using ArchSample.UseCases.Infrastructure;

namespace ArchSample.UseCases.UserManagement.Commands
{
    public record RegisterUserCommand(Email Email, string Username, RegistrationSource Source) : ICommand
    {
    }

    public record RegistrationResult(bool Success, string WelcomeMessage) : IResponse;

    public class RegisterUserHandler : ICommandHandler<RegisterUserCommand, RegistrationResult>
    {
        private readonly IUserService _userService;
        private readonly INotificationsService _notifications;
        private readonly IUnitOfWorkFactory _unitOfWorkFactory;

        public RegisterUserHandler(
            IUserService userService,
            INotificationsService notifications,
            IUnitOfWorkFactory unitOfWorkFactory)
        {
            _userService = userService;
            _notifications = notifications;
            this._unitOfWorkFactory = unitOfWorkFactory;
        }

        public RegistrationResult Handle(RegisterUserCommand command)
        {
            var uow = _unitOfWorkFactory.Create();
            var userId = _userService.RegisterUser(command.Email, command.Username);
            if (command.Source == RegistrationSource.Site)
            {
                _notifications.SendWelcomeEmail(userId);
            }
            uow.SaveChanges();
            return new RegistrationResult(true, $"Welcome! Thanks for registration using {command.Source}");
        }
    }
}

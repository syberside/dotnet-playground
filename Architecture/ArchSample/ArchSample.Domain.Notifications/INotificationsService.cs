using ArchSample.Domain.User;

namespace ArchSample.Domain.Notifications
{
    public interface INotificationsService
    {
        void SendWelcomeEmail(UserId userId);
    }
}

using ArchSample.Domain.User.DataLayer;
using Microsoft.Extensions.DependencyInjection;

namespace ArchSample.Domain.User.Impl.IoC
{
    public static class UserModuleRegistration
    {
        public static IServiceCollection AddUserModule(this IServiceCollection serviceCollection)
        {
            return serviceCollection
                .AddTransient<IUserService, UserService>()
                .AddTransient<IUserRepository, UserRepository>();
        }
    }
}

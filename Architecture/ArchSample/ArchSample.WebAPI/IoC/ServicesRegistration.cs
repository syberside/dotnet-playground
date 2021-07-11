using ArchSample.Domain.User.Impl.IoC;
using ArchSample.Infrastructure.DataLayer;
using ArchSample.UseCases.Infrastructure;
using Microsoft.Extensions.DependencyInjection;

namespace ArchSample.WebAPI.IoC
{
    public static class ServicesRegistration
    {
        public static IServiceCollection AddModules(this IServiceCollection sc)
        {
            return sc
                .AddUserModule()
                .AddTransient<IUnitOfWorkFactory, DbContext>();
        }
    }
}

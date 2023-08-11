using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Tlis.Cms.UserManagement.Application.Mappers;

namespace Tlis.Cms.UserManagement.Application;

public static class DependencyInjection
{
    public static void AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
        });

        services.AddTransient<UserMapper>();
    }
}
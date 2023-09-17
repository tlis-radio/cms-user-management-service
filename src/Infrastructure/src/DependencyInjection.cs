using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.ObjectPool;
using Microsoft.Extensions.Options;
using Tlis.Cms.UserManagement.Infrastructure.Configurations;
using Tlis.Cms.UserManagement.Infrastructure.Persistence;
using Tlis.Cms.UserManagement.Infrastructure.Persistence.Interfaces;
using Tlis.Cms.UserManagement.Infrastructure.PooledObjects;
using Tlis.Cms.UserManagement.Infrastructure.Services;
using Tlis.Cms.UserManagement.Infrastructure.Services.Interfaces;

namespace Tlis.Cms.UserManagement.Infrastructure;

public static class DependencyInjection
{
    public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services
            .AddOptions<ServiceUrlsConfiguration>()
            .Bind(configuration.GetSection("ServiceUrls"))
            .ValidateDataAnnotations()
            .ValidateOnStart();
        services
            .AddOptions<Auth0Configuration>()
            .Bind(configuration.GetSection("Auth0"))
            .ValidateDataAnnotations()
            .ValidateOnStart();
        services
            .AddOptions<RabbitMqConfiguration>()
            .Bind(configuration.GetSection("RabbitMq"))
            .ValidateDataAnnotations()
            .ValidateOnStart();

        services.AddSingleton<ObjectPoolProvider, DefaultObjectPoolProvider>();
        services.AddSingleton(s =>
        {
            var provider = s.GetRequiredService<ObjectPoolProvider>();
            return provider.Create(new RabbitMqModelPooledObject(s.GetRequiredService<IOptions<RabbitMqConfiguration>>()));
        });

        services.AddDbContext<IUserManagementDbContext, UserManagementDbContext>(options =>
            {
                options
                    .UseNpgsql(
                        configuration.GetConnectionString("Postgres"),
                        x => x.MigrationsHistoryTable(
                            HistoryRepository.DefaultTableName, 
                            "cms_user_management"))
                    .UseSnakeCaseNamingConvention();
            },
            contextLifetime: ServiceLifetime.Transient,
            optionsLifetime: ServiceLifetime.Singleton);
        services.AddTransient<IUnitOfWork, UnitOfWork>();

        services.AddScoped<ICacheService, CacheService>();
        services.AddScoped<IRoleService, RoleService>();

        services.AddSingleton<IStorageService, StorageService>();

        services.AddHttpClient<ITokenProviderService, TokenProviderService>();
        services.AddHttpClient<IAuthProviderManagementService, AuthProviderManagementService>();
    }
}
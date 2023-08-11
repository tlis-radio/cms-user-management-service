using Microsoft.AspNetCore.Authentication.JwtBearer;
using Tlis.Cms.UserManagement.Api.Constants;

namespace Tlis.Cms.UserManagement.Api.Extensions;

public static class AuthorizationSetup
{
    public static IServiceCollection ConfigureAuthorization(
        this IServiceCollection services,
        ConfigurationManager configuration)
    {
        services
            .AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.Authority = configuration.GetSection("Jwt").GetValue<string>("Authority");
                options.Audience = configuration.GetSection("Jwt").GetValue<string>("Audience");
                options.SaveToken = true;
            });
        
        services.AddAuthorization(options =>
        {
            options.AddPolicy(
                Policy.UserWrite,
                policy => policy.RequireClaim("permissions", "write:user"));
            options.AddPolicy(
                Policy.UserDelete,
                policy => policy.RequireClaim("permissions", "delete:user"));
        });

        return services;
    }
}
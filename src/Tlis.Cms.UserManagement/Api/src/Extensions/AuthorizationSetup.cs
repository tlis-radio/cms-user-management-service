using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using Tlis.Cms.UserManagement.Api.Constants;

namespace Tlis.Cms.UserManagement.Api.Extensions;

public static class AuthorizationSetup
{
    public static void ConfigureAuthorization(this IServiceCollection services, ConfigurationManager configuration)
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
                options.RequireHttpsMetadata = configuration.GetSection("Jwt").GetValue<bool>("RequireHttpsMetadata");
                options.SaveToken = true;
            });
        
        services.AddAuthorizationBuilder()
            .AddPolicy(Policy.UserWrite, policy => policy.RequireClaim("permissions", "write:user"))
            .AddPolicy(Policy.UserDelete, policy => policy.RequireClaim("permissions", "delete:user"))
            .AddPolicy(Policy.UserRead, policy => policy.RequireClaim("permissions", "read:user"));
    }
}
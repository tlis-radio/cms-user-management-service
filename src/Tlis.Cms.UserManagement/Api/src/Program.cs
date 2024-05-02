using System.Text.Json.Serialization;
using Tlis.Cms.UserManagement.Api.Extensions;
using Tlis.Cms.UserManagement.Application;
using Tlis.Cms.UserManagement.Infrastructure;

namespace Tlis.Cms.UserManagement.Api
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services
                .AddControllers()
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
                });

            builder.Services.ConfigureProblemDetails();
            builder.Services.ConfigureSwagger();
            builder.Services.ConfigureAuthorization(builder.Configuration);

            builder.Logging.ConfigureOtel(builder.Environment);
            builder.Services.ConfigureOtel(builder.Environment);
            builder.Services.AddMemoryCache();

            builder.Services.AddApplication();
            builder.Services.AddInfrastructure(builder.Configuration);

            var app = builder.Build();
            
            app.UseExceptionHandler();
            app.UseStatusCodePages();

            app.UseSwagger();
            app.UseSwaggerUI();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}


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

            // Add services to the container.
            builder.Services.AddMemoryCache();
            builder.Services.AddControllers();

            builder.Services.ConfigureSwagger();

            builder.Services.ConfigureAuthorization(builder.Configuration);
            builder.Services.AddApplication();
            builder.Services.AddInfrastructure(builder.Configuration);

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}


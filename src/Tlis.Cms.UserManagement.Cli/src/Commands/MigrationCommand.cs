using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Tlis.Cms.UserManagement.Cli.Commands.Base;
using Tlis.Cms.UserManagement.Infrastructure.Persistence.Interfaces;

namespace Tlis.Cms.UserManagement.Cli.Commands;

public class MigrationCommand(IUserManagementDbContext dbContext, ILogger<MigrationCommand> logger)
    : BaseCommand("migration", "Run DB migration", logger)
{
    protected override async Task TryHandleCommand()
    {
        var pendingMigrations = await dbContext.Database.GetPendingMigrationsAsync();

        if (pendingMigrations.Any())
        {
            _logger.LogInformation(
                $"Applying migrations: {string.Join(',', pendingMigrations)}"
            );

            await dbContext.Database.MigrateAsync();
        }
        else
        {
            _logger.LogInformation("No migrations to execute");
        }
    }
}

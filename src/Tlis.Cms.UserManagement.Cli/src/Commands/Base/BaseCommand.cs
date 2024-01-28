using System;
using System.CommandLine;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace Tlis.Cms.UserManagement.Cli.Commands.Base;

public abstract class BaseCommand : Command
{
    protected readonly ILogger<BaseCommand> _logger;

    public BaseCommand(string name, string description, ILogger<BaseCommand> logger)
        : base(name, description)
    {
        _logger = logger;
        this.SetHandler(HandleCommand);
    }

    protected async Task<int> HandleCommand()
    {
        try
        {
            await TryHandleCommand();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            return 1;
        }

        return 0;
    }

    protected abstract Task TryHandleCommand();
}

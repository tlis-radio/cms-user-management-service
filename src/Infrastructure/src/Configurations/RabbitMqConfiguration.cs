using System.ComponentModel.DataAnnotations;

namespace Tlis.Cms.UserManagement.Infrastructure.Configurations;

internal sealed class RabbitMqConfiguration
{
    [Required(AllowEmptyStrings = false)]
    public string HostName { get; set; } = null!;

    [Required(AllowEmptyStrings = false)]
    public string UserName { get; set; } = null!;

    [Required(AllowEmptyStrings = false)]
    public string Password { get; set; } = null!;
}
using System.ComponentModel.DataAnnotations;

namespace Tlis.Cms.UserManagement.Infrastructure.Configurations;

internal sealed class Auth0Configuration
{
    [Required(AllowEmptyStrings = false)]
    public string Domain { get; set; } = null!;

    [Required(AllowEmptyStrings = false)]
    public string ClientId { get; set; } = null!;

    [Required(AllowEmptyStrings = false)]
    public string ClientSecret { get; set; } = null!;
}
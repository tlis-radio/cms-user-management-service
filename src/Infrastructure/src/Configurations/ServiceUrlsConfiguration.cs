using System.ComponentModel.DataAnnotations;

namespace Tlis.Cms.UserManagement.Infrastructure.Configurations;

internal sealed class ServiceUrlsConfiguration
{
    [Required(AllowEmptyStrings = false)]
    public string StorageAccount { get; set; } = null!;
}
using System.ComponentModel.DataAnnotations;

namespace Tlis.Cms.UserManagement.Application.Contracts.Api.Responses;

public sealed class UserPaginationGetResponseRole
{
    [Required]
    public string Name { get; set; } = null!;
}
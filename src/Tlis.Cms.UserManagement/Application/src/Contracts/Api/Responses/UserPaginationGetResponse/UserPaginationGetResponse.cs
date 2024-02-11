using System;
using System.ComponentModel.DataAnnotations;

namespace Tlis.Cms.UserManagement.Application.Contracts.Api.Responses;

public sealed class UserPaginationGetResponse
{
    [Required]
    public Guid Id { get; set; }

    [Required]
    public string Firstname { get; set; } = null!;

    [Required]
    public string Lastname { get; set; } = null!;

    [Required]
    public string Nickname { get; set; } = null!;

    [Required]
    public bool IsActive { get; set; }

    public string? Email { get; set; }
}
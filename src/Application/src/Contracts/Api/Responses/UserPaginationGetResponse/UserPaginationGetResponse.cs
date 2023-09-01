using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Tlis.Cms.UserManagement.Application.Contracts.Api.Responses;

public sealed class UserPaginationGetResponse
{
    [Required]
    public string Firstname { get; set; } = null!;

    [Required]
    public string Lastname { get; set; } = null!;

    [Required]
    public string Nickname { get; set; } = null!;

    [Required]
    public string Description { get; set; } = null!;

    [Required]
    public string ProfileImageUrl { get; set; } = null!;

    [Required]
    public bool? IsActive { get; set; }

    [Required]
    public DateOnly MemberSinceDate { get; set; }

    public DateOnly? MembershipEndedDate { get; set; }

    public string? MembershipEndedReason { get; set; }

    public string? ExternalId { get; set; }

    public string? Email { get; set; }

    [Required]
    public IEnumerable<UserPaginationGetResponseUserRoleHistory> RoleHistory { get; set; } = new List<UserPaginationGetResponseUserRoleHistory>();
}
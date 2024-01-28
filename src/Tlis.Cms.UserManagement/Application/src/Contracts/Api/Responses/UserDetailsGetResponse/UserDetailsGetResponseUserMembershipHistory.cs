using System;
using System.ComponentModel.DataAnnotations;
using Tlis.Cms.UserManagement.Domain.Constants;

namespace Tlis.Cms.UserManagement.Application.Contracts.Api.Responses;

public sealed class UserDetailsGetResponseUserMembershipHistory
{
    [Required]
    public MembershipStatus Status { get; set; }

    [Required]
    public DateTime ChangeDate { get; set; }

    public string? Description { get; set; }
}
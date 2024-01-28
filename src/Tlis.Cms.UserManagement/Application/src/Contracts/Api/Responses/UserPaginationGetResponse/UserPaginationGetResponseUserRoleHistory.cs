using System;
using System.ComponentModel.DataAnnotations;

namespace Tlis.Cms.UserManagement.Application.Contracts.Api.Responses;

public sealed class UserPaginationGetResponseUserRoleHistory
{
    [Required]
    public DateOnly FunctionStartDate { get; set; }

    public DateOnly? FunctionEndDate { get; set; }

    [Required]
    public UserPaginationGetResponseRole? Role { get; set; }

    public string? Description { get; set; }
}
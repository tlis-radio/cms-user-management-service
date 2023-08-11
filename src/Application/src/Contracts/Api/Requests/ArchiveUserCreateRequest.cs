using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using MediatR;
using Swashbuckle.AspNetCore.Annotations;
using Tlis.Cms.UserManagement.Application.Contracts.Api.Responses;

namespace Tlis.Cms.UserManagement.Application.Contracts.Api.Requests;

public sealed class ArchiveUserCreateRequest : IRequest<BaseCreateResponse>
{
    [SwaggerSchema(Description = "User's first name")]
    [Required]
    public required string Firstname { get; set; }

    [SwaggerSchema(Description = "User's last name")]
    [Required]
    public required string Lastname { get; set; }

    [SwaggerSchema(Description = "User's nickname or alias")]
    [Required]
    public required string Nickname { get; set; }

    [SwaggerSchema(Description = "User's description or bio")]
    [Required]
    public required string Description { get; set; }

    [SwaggerSchema(Description = "The date on which the user became a member of TLIS")]
    [Required]
    public required DateOnly MemberSinceDate { get; set; }

    [SwaggerSchema(Description = "The date on which the user ended his membership in TLIS")]
    [Required]
    public required DateOnly MembershipEndedDate { get; set; }

    [SwaggerSchema(Description = "Reason why date user ended his membership in TLIS")]
    [Required]
    public required string MembershipEndedReason { get; set; }

    [SwaggerSchema(Description = "User's email address")]
    public string? Email { get; set; }

    [SwaggerSchema(Description = "User's password. If no password is provided user wont be able to login to admin.")]
    public string? Password { get; set; }

    [SwaggerSchema(Description = "User's role history.")]
    [Required]
    public required IEnumerable<ArchiveUserRoleHistoryCreateRequest> RoleHistory { get; set; }
}

public sealed class ArchiveUserRoleHistoryCreateRequest
{
    [SwaggerSchema(Description = "Name of the role.")]
    [Required]
    public Guid RoleId { get; set; }

    [SwaggerSchema(Description = "The starting date of when user started this position.")]
    [Required]
    public DateOnly FunctionStartDate { get; set; }

    [SwaggerSchema(Description = "The ending date of when user started this position.")]
    public DateOnly? FunctionEndDate { get; set; }
}
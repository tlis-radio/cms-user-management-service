using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using MediatR;
using Swashbuckle.AspNetCore.Annotations;
using Tlis.Cms.UserManagement.Application.Contracts.Api.Responses;

namespace Tlis.Cms.UserManagement.Application.Contracts.Api.Requests;

public sealed class UserCreateRequest : IRequest<BaseCreateResponse>
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

    [SwaggerSchema(Description = "User's date on which the user became a member of TLIS.")]
    [Required]
    public DateOnly MemberSinceDate { get; set; }

    [SwaggerSchema(Description = "User's email address")]
    public string? Email { get; set; }

    [SwaggerSchema(Description = "User's password. If no password is provided user wont be able to login to admin.")]
    public string? Password { get; set; }

    [SwaggerSchema(Description = "The user's role or permission level within the service or platform.")]
    [Required]
    public required Guid RoleId { get; set; }

    [SwaggerSchema(Description = "The date on which the user began their current role or position within TLIS.")]
    [Required]
    public DateOnly FunctionStartDate { get; set; }
}
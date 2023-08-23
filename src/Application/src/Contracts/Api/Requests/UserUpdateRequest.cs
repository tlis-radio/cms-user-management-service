using System;
using System.ComponentModel;
using System.Text.Json.Serialization;
using MediatR;
using Swashbuckle.AspNetCore.Annotations;

namespace Tlis.Cms.UserManagement.Application.Contracts.Api.Requests;

public sealed class UserUpdateRequest : IRequest<bool>
{
    [JsonIgnore]
    public Guid Id { get; set; }

    [SwaggerSchema(Description = "User's first name")]
    [DefaultValue(null)]
    public string? Firstname { get; set; }

    [SwaggerSchema(Description = "User's last name")]
    [DefaultValue(null)]
    public string? Lastname { get; set; }

    [SwaggerSchema(Description = "User's nickname or alias")]
    [DefaultValue(null)]
    public string? Nickname { get; set; }

    [SwaggerSchema(Description = "User's description or bio")]
    public string? Description { get; set; }

    [SwaggerSchema(Description = "Is user archive member")]
    public bool? IsActive { get; set; }

    [SwaggerSchema(Description = "The date on which the user became a member of TLIS.")]
    public DateOnly? MemberSinceDate { get; set; }

    [SwaggerSchema(Description = "The ending date of when user became inactive.")]
    public DateOnly? MembershipEndedDate { get; set; }

    [SwaggerSchema(Description = "The reason why user ended his presence in TLIS.")]
    public string? MembershipEndedReason { get; set; }

    [SwaggerSchema(Description = "User's email address")]
    public string? Email { get; set; }
}
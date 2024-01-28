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
}
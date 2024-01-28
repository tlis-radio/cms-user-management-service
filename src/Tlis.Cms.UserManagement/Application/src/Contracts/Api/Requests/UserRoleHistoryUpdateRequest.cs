using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using MediatR;
using Swashbuckle.AspNetCore.Annotations;

namespace Tlis.Cms.UserManagement.Application.Contracts.Api.Requests;

public sealed class UserRoleHistoryUpdateRequest : IRequest<bool>
{
    [JsonIgnore]
    public Guid Id { get; set; }

    [JsonIgnore]
    public Guid HistoryId { get; set; }

    [SwaggerSchema(Description = "The user's role or permission level within the service or platform.")]
    [Required]
    public required Guid RoleId { get; set; }

    [SwaggerSchema(Description = "The date on which the user began their current role or position within TLIS.")]
    [Required]
    public required DateTime FunctionStartDate { get; set; }

    [SwaggerSchema(Description = "The ending date of when user started this position.")]
    public DateTime? FunctionEndDate { get; set; }

    [SwaggerSchema(Description = "Description why this position was given to user.")]
    public string? Description { get; set; }
}
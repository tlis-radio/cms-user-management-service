using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using MediatR;
using Swashbuckle.AspNetCore.Annotations;
using Tlis.Cms.UserManagement.Application.Contracts.Api.Responses;

namespace Tlis.Cms.UserManagement.Application.Contracts.Api.Requests;

public sealed class RoleHistoryToUserAddRequest : IRequest<BaseCreateResponse?>
{
    [JsonIgnore]
    public Guid Id { get; set; }

    [SwaggerSchema(Description = "The user's role or permission level within the service or platform.")]
    [Required]
    public Guid RoleId { get; set; }

    [SwaggerSchema(Description = "The date on which the user began their current role or position within TLIS.")]
    [Required]
    public DateOnly FunctionStartDate { get; set; }

    [SwaggerSchema(Description = "The ending date of when user started this position.")]
    public DateOnly? FunctionEndDate { get; set; }
}
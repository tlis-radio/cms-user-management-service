using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using MediatR;
using Tlis.Cms.UserManagement.Domain.Constants;

namespace Tlis.Cms.UserManagement.Application.Contracts.Api.Requests;

public sealed class UserMembershipUpdateRequest : IRequest<bool>
{
    [JsonIgnore]
    public Guid UserId { get; set; }

    [Required]
    public MembershipStatus Status { get; set; }

    [Required]
    public DateTime ChangeDate { get; set; }

    [Required]
    public string? Description { get; set; }
}
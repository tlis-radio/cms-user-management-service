using System;
using System.Text.Json.Serialization;
using MediatR;
using Tlis.Cms.UserManagement.Application.Contracts.Api.Responses;

namespace Tlis.Cms.UserManagement.Application.Contracts.Api.Requests;

public sealed class MembershipHistoryToUserAddRequest : IRequest<BaseCreateResponse?>
{
    [JsonIgnore]
    public Guid UserId { get; set; }

    [JsonRequired]
    public Guid MembershipId { get; set; }

    public string? Description { get; set; }

    [JsonRequired]
    public DateTime ChangeDate { get; set; }
}
using System;
using System.Text.Json.Serialization;

namespace Tlis.Cms.UserManagement.Application.Contracts.Rpc.Responses;

public sealed class GetUsersNickNameResponseResult
{
    [JsonRequired]
    public Guid Id { get; set; }

    [JsonRequired]
    public string NickName { get; set; } = null!;
}
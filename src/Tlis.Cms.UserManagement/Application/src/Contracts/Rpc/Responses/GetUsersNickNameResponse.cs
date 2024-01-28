using System.Collections.Generic;

namespace Tlis.Cms.UserManagement.Application.Contracts.Rpc.Responses;

public sealed class GetUsersNickNameResponse
{
    public List<GetUsersNickNameResponseResult> Results { get; set; } = [];
}
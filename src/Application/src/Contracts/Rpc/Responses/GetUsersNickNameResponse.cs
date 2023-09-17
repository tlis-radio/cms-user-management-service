using System.Collections.Generic;

namespace Tlis.Cms.UserManagement.Application.Contracts.Rpc.Responses;

public sealed class GetUsersNickNameResponse
{
    public IList<GetUsersNickNameResponseResult> Results { get; set; } = new List<GetUsersNickNameResponseResult>();
}
using System.Collections.Generic;

namespace Tlis.Cms.UserManagement.Application.Contracts.Api.Responses;

public sealed class RoleGetAllResponse
{
    public IList<RoleGetAllResponseItem> Results { get; set; } = new List<RoleGetAllResponseItem>();
}


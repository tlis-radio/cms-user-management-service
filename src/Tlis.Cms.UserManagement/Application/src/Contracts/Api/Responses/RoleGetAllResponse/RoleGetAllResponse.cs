using System.Collections.Generic;

namespace Tlis.Cms.UserManagement.Application.Contracts.Api.Responses;

public sealed class RoleGetAllResponse
{
    public List<RoleGetAllResponseItem> Results { get; set; } = [];
}


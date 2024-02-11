using System.Collections.Generic;

namespace Tlis.Cms.UserManagement.Application.Contracts.Api.Responses;

public sealed class MembershipStatusGetAllResponse
{
    public IList<MembershipStatusGetAllResponseItem> Results { get; set; } = [];
}


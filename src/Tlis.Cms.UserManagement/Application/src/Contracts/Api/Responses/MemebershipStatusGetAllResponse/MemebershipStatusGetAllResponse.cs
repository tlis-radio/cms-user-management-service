using System.Collections.Generic;

namespace Tlis.Cms.UserManagement.Application.Contracts.Api.Responses;

public sealed class MemebershipStatusGetAllResponse
{
    public IList<MemebershipStatusGetAllResponseItem> Results { get; set; } = [];
}


using System;
using System.Collections.Generic;

namespace Tlis.Cms.UserManagement.Application.Contracts.Rpc.Requests;

public sealed class GetUsersNickNameRequest
{
    public IList<Guid> Ids { get; set; } = new List<Guid>();
}
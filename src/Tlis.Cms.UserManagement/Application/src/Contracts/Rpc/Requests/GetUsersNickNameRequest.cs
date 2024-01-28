using System;
using System.Collections.Generic;

namespace Tlis.Cms.UserManagement.Application.Contracts.Rpc.Requests;

public sealed class GetUsersNickNameRequest
{
    public List<Guid> Ids { get; set; } = [];
}
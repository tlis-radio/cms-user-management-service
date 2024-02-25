using System;
using System.Collections.Generic;
using MediatR;
using Tlis.Cms.UserManagement.Application.Contracts.Api.Responses;

namespace Tlis.Cms.UserManagement.Application.Contracts.Api.Requests;

public sealed class UserFilterGetRequest : IRequest<FilterResponse<UserFilterGetResponse>>
{
    public List<Guid> UserIds { get; set; } = [];
}
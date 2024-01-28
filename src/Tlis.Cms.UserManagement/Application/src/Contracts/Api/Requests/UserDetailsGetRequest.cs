using System;
using MediatR;
using Tlis.Cms.UserManagement.Application.Contracts.Api.Responses;

namespace Tlis.Cms.UserManagement.Application.Contracts.Api.Requests;

public sealed class UserDetailsGetRequest : IRequest<UserDetailsGetResponse?>
{
    public Guid Id { get; set; }
}
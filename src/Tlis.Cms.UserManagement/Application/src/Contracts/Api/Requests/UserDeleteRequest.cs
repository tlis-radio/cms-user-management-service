using System;
using MediatR;

namespace Tlis.Cms.UserManagement.Application.Contracts.Api.Requests;

public sealed class UserDeleteRequest : IRequest<bool>
{
    public Guid Id { get; set; }
}
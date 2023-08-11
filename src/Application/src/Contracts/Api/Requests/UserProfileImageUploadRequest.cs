using System;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Tlis.Cms.UserManagement.Application.Contracts.Api.Requests;

public sealed class UserProfileImageUploadRequest : IRequest<bool>
{
    public required Guid Id { get; set; }

    public required IFormFile Image { get; set; }
}
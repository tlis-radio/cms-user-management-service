using System;
using MediatR;
using Microsoft.AspNetCore.Http;
using Tlis.Cms.UserManagement.Application.Contracts.Api.Responses;

namespace Tlis.Cms.UserManagement.Application.Contracts.Api.Requests;

public sealed class UserProfileImageUploadRequest : IRequest<BaseCreateResponse?>
{
    public required Guid Id { get; set; }

    public required IFormFile Image { get; set; }
}
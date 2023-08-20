using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Net.Mime;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Tlis.Cms.UserManagement.Api.Constants;
using Tlis.Cms.UserManagement.Api.Controllers.Attributes;
using Tlis.Cms.UserManagement.Api.Controllers.Base;
using Tlis.Cms.UserManagement.Application.Contracts.Api.Requests;
using Tlis.Cms.UserManagement.Application.Contracts.Api.Responses;

namespace Tlis.Cms.UserManagement.Api.Controllers;

[ApiController]
[Route("[controller]")]
public sealed class UserController : BaseController
{
    public UserController(IMediator mediator) : base(mediator)
    {
    }

    [HttpGet("{id}")]
    [Authorize(Policy.UserRead)]
    [SwaggerOperation("Get user's details")]
    [Produces(MediaTypeNames.Application.Json)]
    public ValueTask<ActionResult<UserDetailsGetResponse>> GetUserDetails([FromRoute] Guid id)
        => HandleGet(new UserDetailsGetRequest { Id = id });

    [HttpPost]
    [Authorize(Policy.UserWrite)]
    [Consumes(MediaTypeNames.Application.Json)]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType((int)HttpStatusCode.Created)]
    [SwaggerOperation("Create new active user.", "Crate new user with <code>IsActive</code> property set to <code>true</code>")]
    public ValueTask<ActionResult<BaseCreateResponse>> CreateUser([FromBody, Required] UserCreateRequest request)
        => HandlePost(request);

    [HttpPost("archive")]
    [Authorize(Policy.UserWrite)]
    [Consumes(MediaTypeNames.Application.Json)]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType((int)HttpStatusCode.Created)]
    [SwaggerOperation("Create new archive user.", "Crate new user with <code>IsActive</code> property set to <code>false</code>")]
    public ValueTask<ActionResult<BaseCreateResponse>> ArchiveCreateUser([FromBody, Required] ArchiveUserCreateRequest request)
        => HandlePost(request);

    [HttpPost("{id}/role/history")]
    [Authorize(Policy.UserWrite)]
    [Consumes(MediaTypeNames.Application.Json)]
    [Produces(MediaTypeNames.Application.Json)]
    [SwaggerOperation("Add new entry into user's role history")]
    public ValueTask<ActionResult<BaseCreateResponse>> AddRoleHistoryToUser(
        [FromRoute] Guid id,
        [FromBody, Required] RoleHistoryToUserAddRequest request)
    {
        request.Id = id;

        return HandlePost(request);
    }

    [HttpPost("{id}/profile-image")]
    [Authorize(Policy.UserWrite)]
    [RequestSizeLimit(5000000)]
    [FormFileContentTypeFilter(ContentType = "image/jpeg,image/png")]
    [SwaggerOperation("Upload user's profile image.",
        "If user already has an image current profile image will be deleted and replaced with this new image. Maximal allowed size is 5 Megabyte.")]
    public ValueTask<ActionResult<BaseCreateResponse>> UploadProfileImage([FromRoute] Guid id, IFormFile image)
        => HandlePost(new UserProfileImageUploadRequest { Id = id, Image = image });

    [HttpPut("{id}/role/history/{historyId}")]
    [Authorize(Policy.UserWrite)]
    [Consumes(MediaTypeNames.Application.Json)]
    [SwaggerOperation("Update user's existing role history")]
    public ValueTask<ActionResult> UpdateUserRoleHistory(
        [FromRoute] Guid id,
        [FromRoute] Guid historyId,
        [FromBody, Required] UserRoleHistoryUpdateRequest request)
    {
        request.Id = id;
        request.HistoryId = historyId;

        return HandlePut(request);
    }

    [HttpPut("{id}")]
    [Authorize(Policy.UserWrite)]
    [Consumes(MediaTypeNames.Application.Json)]
    [SwaggerOperation("Update user's details")]
    public ValueTask<ActionResult> UpdateUser(
        [FromRoute] Guid id,
        [FromBody] UserUpdateRequest request)
    {
        request.Id = id;

        return HandlePut(request);
    }

    [HttpDelete("{id}")]
    [SwaggerOperation("Delete user")]
    public ValueTask<ActionResult> DeleteUser([FromRoute] Guid id)
        => HandleDelete(new UserDeleteRequest { Id = id });
}
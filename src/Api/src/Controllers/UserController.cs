using System.ComponentModel.DataAnnotations;
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

    [HttpGet("{id:guid}")]
    [Authorize(Policy.UserRead)]
    [SwaggerOperation("Get user's details")]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(typeof(UserDetailsGetResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public ValueTask<ActionResult<UserDetailsGetResponse>> GetUserDetails([FromRoute] Guid id)
        => HandleGet(new UserDetailsGetRequest { Id = id });

    [HttpPost]
    [Authorize(Policy.UserWrite)]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    [SwaggerOperation("Create new active user.", "Crate new user with <code>IsActive</code> property set to <code>true</code>")]
    public ValueTask<ActionResult<BaseCreateResponse>> CreateUser([FromBody, Required] UserCreateRequest request)
        => HandlePost(request);

    [HttpPost("archive")]
    [Authorize(Policy.UserWrite)]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    [SwaggerOperation("Create new archive user.", "Crate new user with <code>IsActive</code> property set to <code>false</code>")]
    public ValueTask<ActionResult<BaseCreateResponse>> ArchiveCreateUser([FromBody, Required] ArchiveUserCreateRequest request)
        => HandlePost(request);

    [HttpPost("{id:guid}/role/history")]
    [Authorize(Policy.UserWrite)]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    [SwaggerOperation("Add new entry into user's role history")]
    public ValueTask<ActionResult<BaseCreateResponse>> AddRoleHistoryToUser(
        [FromRoute] Guid id,
        [FromBody, Required] RoleHistoryToUserAddRequest request)
    {
        request.Id = id;

        return HandlePost(request);
    }

    [HttpPost("{id:guid}/profile-image")]
    [Authorize(Policy.UserWrite)]
    [RequestSizeLimit(5000000)]
    [FormFileContentTypeFilter(ContentType = "image/jpeg,image/png")]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    [SwaggerOperation("Upload user's profile image.",
        "If user already has an image current profile image will be deleted and replaced with this new image. Maximal allowed size is 5 Megabyte.")]
    public ValueTask<ActionResult<BaseCreateResponse>> UploadProfileImage([FromRoute] Guid id, IFormFile image)
        => HandlePost(new UserProfileImageUploadRequest { Id = id, Image = image });

    [HttpPut("{id:guid}/role/history/{historyId:guid}")]
    [Authorize(Policy.UserWrite)]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
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

    [HttpPut("{id:guid}")]
    [Authorize(Policy.UserWrite)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    [SwaggerOperation("Update user's details")]
    public ValueTask<ActionResult> UpdateUser([FromRoute] Guid id, [FromBody, Required] UserUpdateRequest request)
    {
        request.Id = id;

        return HandlePut(request);
    }

    [HttpDelete("{id:guid}")]
    [Authorize(Policy.UserDelete)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    [SwaggerOperation("Delete user")]
    public ValueTask<ActionResult> DeleteUser([FromRoute] Guid id)
        => HandleDelete(new UserDeleteRequest { Id = id });
}
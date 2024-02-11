using System.ComponentModel.DataAnnotations;
using System.Net.Mime;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Tlis.Cms.UserManagement.Api.Constants;
using Tlis.Cms.UserManagement.Application.Contracts.Api.Requests;
using Tlis.Cms.UserManagement.Application.Contracts.Api.Responses;

namespace Tlis.Cms.UserManagement.Api.Controllers;

[ApiController]
[Route("[controller]")]
public sealed class UserController(IMediator mediator) : ControllerBase
{
    [HttpGet("{id:guid}")]
    [Authorize(Policy.UserRead)]
    [SwaggerOperation("Get user's details")]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(typeof(UserDetailsGetResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async ValueTask<ActionResult<UserDetailsGetResponse>> GetUserDetails([FromRoute] Guid id)
    {
        var response = await mediator.Send(new UserDetailsGetRequest { Id = id });

        return response is null
            ? NotFound()
            : Ok(response);
    }

    [HttpGet("pagination")]
    [AllowAnonymous]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(typeof(PaginationResponse<UserPaginationGetResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    [SwaggerOperation("Paging user's")]
    public async ValueTask<ActionResult<PaginationResponse<UserPaginationGetResponse>>> Pagination([FromQuery] UserPaginationGetRequest request)
    {
        var response = await mediator.Send(request);

        return response is null
            ? NotFound()
            : Ok(response);
    }

    [HttpPost]
    [Authorize(Policy.UserWrite)]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    [SwaggerOperation("Create new active user.", "Crate new user with <code>IsActive</code> property set to <code>true</code>")]
    public async ValueTask<ActionResult<BaseCreateResponse>> CreateUser([FromBody, Required] UserCreateRequest request)
    {
        var response = await mediator.Send(request);

        return response is null
            ? BadRequest()
            : CreatedAtAction(nameof(GetUserDetails), new { response.Id } , response);
    }

    [HttpPost("archive")]
    [Authorize(Policy.UserWrite)]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    [SwaggerOperation("Create new archive user.", "Crate new user with <code>IsActive</code> property set to <code>false</code>")]
    public async ValueTask<ActionResult<BaseCreateResponse>> ArchiveCreateUser([FromBody, Required] ArchiveUserCreateRequest request)
    {
        var response = await mediator.Send(request);

        return response is null
            ? BadRequest()
            : CreatedAtAction(nameof(GetUserDetails), new { response.Id } , response);
    }

    [HttpPost("{id:guid}/role/history")]
    [Authorize(Policy.UserWrite)]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    [SwaggerOperation("Add new entry into user's role history")]
    public async ValueTask<ActionResult<BaseCreateResponse>> AddRoleHistoryToUser(
        [FromRoute] Guid id,
        [FromBody, Required] RoleHistoryToUserAddRequest request)
    {
        request.Id = id;

        var response = await mediator.Send(request);

        return response is null ? BadRequest() : NoContent();
    }

    [HttpPut("{id:guid}/role/history/{historyId:guid}")]
    [Authorize(Policy.UserWrite)]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    [SwaggerOperation("Update user's existing role history")]
    public async ValueTask<ActionResult> UpdateUserRoleHistory(
        [FromRoute] Guid id,
        [FromRoute] Guid historyId,
        [FromBody, Required] UserRoleHistoryUpdateRequest request)
    {
        request.Id = id;
        request.HistoryId = historyId;

        var response = await mediator.Send(request);

        return response ? NoContent() : BadRequest();
    }

    [HttpPut("{id:guid}")]
    [Authorize(Policy.UserWrite)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    [SwaggerOperation("Update user's details")]
    public async ValueTask<ActionResult> UpdateUser([FromRoute] Guid id, [FromBody, Required] UserUpdateRequest request)
    {
        request.Id = id;

        var response = await mediator.Send(request);

        return response ? NoContent() : BadRequest();
    }

    [HttpPut("{id:guid}/membership")]
    [Authorize(Policy.UserWrite)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    [SwaggerOperation("Update user's membership")]
    public async ValueTask<ActionResult> UpdateUserMembership([FromRoute] Guid id, [FromBody, Required] UserMembershipUpdateRequest request)
    {
        request.UserId = id;

        var response = await mediator.Send(request);

        return response ? NoContent() : BadRequest();
    }

    [HttpDelete("{id:guid}")]
    [Authorize(Policy.UserDelete)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    [SwaggerOperation("Delete user")]
    public async ValueTask<ActionResult> DeleteUser([FromRoute] Guid id)
    {
        var response = await mediator.Send(new UserDeleteRequest { Id = id });

        return response ? NotFound() : NoContent();
    }
}
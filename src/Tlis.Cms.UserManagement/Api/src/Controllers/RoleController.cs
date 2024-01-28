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
public sealed class RoleController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    [Authorize(Policy.UserRead)]
    [SwaggerOperation("Get all roles")]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(typeof(RoleGetAllResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async ValueTask<ActionResult<RoleGetAllResponse>> GetAll()
    {
        var response = await mediator.Send(new RoleGetAllRequest());

        return response is null
            ? NotFound()
            : Ok(response);
    }
}
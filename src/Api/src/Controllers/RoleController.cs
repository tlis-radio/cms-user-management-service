using System.Net.Mime;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Tlis.Cms.UserManagement.Api.Constants;
using Tlis.Cms.UserManagement.Api.Controllers.Base;
using Tlis.Cms.UserManagement.Application.Contracts.Api.Requests;
using Tlis.Cms.UserManagement.Application.Contracts.Api.Responses;

namespace Tlis.Cms.UserManagement.Api.Controllers;

[ApiController]
[Route("[controller]")]
public sealed class RoleController : BaseController
{
    public RoleController(IMediator mediator) : base(mediator)
    {
    }

    [HttpGet]
    [Authorize(Policy.UserRead)]
    [SwaggerOperation("Get all roles")]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(typeof(RoleGetAllResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public ValueTask<ActionResult<RoleGetAllResponse>> GetAll() => HandleGet(new RoleGetAllRequest());
}
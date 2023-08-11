using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Net.Mime;
using System.Text;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Tlis.Cms.UserManagement.Api.Constants;
using Tlis.Cms.UserManagement.Api.Controllers.Attributes;
using Tlis.Cms.UserManagement.Application.Contracts.Api.Requests;
using Tlis.Cms.UserManagement.Application.Contracts.Api.Responses;

namespace Tlis.Cms.UserManagement.Api.Controllers;

[ApiController]
[Route("[controller]")]
public sealed class UserController : ControllerBase
{
    private readonly IMediator _mediator;
    
    public UserController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    [Authorize(Policy.UserWrite)]
    [Consumes(MediaTypeNames.Application.Json)]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType((int)HttpStatusCode.Created)]
    [SwaggerOperation("Create new active user.",
        "Crate new user with <code>IsActive</code> property set to <code>true</code>")]
    public async Task<ActionResult<BaseCreateResponse>> CreateUser(
        [FromBody, Required] UserCreateRequest request
    )
    {
        var response = await _mediator.Send(request);
        return Created(GetControllerUrl(response.Id), response);
    }

    [HttpPost("archive")]
    [Authorize(Policy.UserWrite)]
    [Consumes(MediaTypeNames.Application.Json)]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType((int)HttpStatusCode.Created)]
    [SwaggerOperation("Create new archive user.", "Crate new user with <code>IsActive</code> property set to <code>false</code>")]
    public async Task<ActionResult<BaseCreateResponse>> ArchiveCreateUser(
        [FromBody, Required] ArchiveUserCreateRequest request
    )
    {   
        var response = await _mediator.Send(request);
        return Created(GetControllerUrl(response.Id), response);
    }

    [HttpPost("{id}/profile-image")]
    [Authorize(Policy.UserWrite)]
    [RequestSizeLimit(5000000)]
    [FormFileContentTypeFilter(ContentType = "image/jpeg,image/png")]
    [SwaggerOperation("Upload user's profile image.",
        "If user already has an image current profile image will be deleted and replaced with this new image. Maximal allowed size is 5 Megabyte.")]
    public async Task<ActionResult> UploadProfileImage([FromRoute] Guid id, IFormFile image)
    {
        if  (await _mediator.Send(new UserProfileImageUploadRequest { Id = id, Image = image }))
        {
            return Ok();
        }

        return BadRequest();
    }
    
    private string GetControllerUrl(Guid id)
    {
        var scheme = Request.Scheme;
        var host = Request.Host.Value;
        var pathBase = Request.PathBase.Value ?? string.Empty;
        var path = Request.RouteValues["controller"]?.ToString() ?? string.Empty;
        var resourceId = id.ToString();
        var pathDelimiter = "/";

        var length = scheme.Length + Uri.SchemeDelimiter.Length + host.Length
                     + pathBase.Length + pathDelimiter.Length + path.Length
                     + pathDelimiter.Length + resourceId.Length;

        return new StringBuilder(length)
            .Append(scheme)
            .Append(Uri.SchemeDelimiter)
            .Append(host)
            .Append(pathBase)
            .Append(pathDelimiter)
            .Append(path)
            .Append(pathDelimiter)
            .Append(resourceId)
            .ToString();
    }
}
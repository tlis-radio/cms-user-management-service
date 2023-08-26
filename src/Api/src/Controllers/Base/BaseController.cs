using System.Text;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Tlis.Cms.UserManagement.Application.Contracts.Api.Responses;

namespace Tlis.Cms.UserManagement.Api.Controllers.Base;

public abstract class BaseController : ControllerBase
{
    private readonly IMediator _mediator;

    protected BaseController(IMediator mediator)
    {
        _mediator = mediator;
    }

    protected async ValueTask<ActionResult<TResponse>> HandleGet<TResponse>(IRequest<TResponse?> request)
    {
        var response = await _mediator.Send(request);

        return response is null
            ? NotFound()
            : Ok(response);
    }

    protected async ValueTask<ActionResult<BaseCreateResponse>> HandlePost(IRequest<BaseCreateResponse?> request)
    {
        var response = await _mediator.Send(request);

        return response is null
            ? BadRequest()
            : Created(GetControllerUrl(response.Id), response);
    }

    protected async ValueTask<ActionResult> HandlePut(IRequest<bool> request)
    {
        var response = await _mediator.Send(request);

        return response
            ? NoContent()
            : NotFound();
    }

    protected async ValueTask<ActionResult> HandleDelete(IRequest<bool> request)
    {
        var response = await _mediator.Send(request);

        return response
            ? NoContent()
            : BadRequest();
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
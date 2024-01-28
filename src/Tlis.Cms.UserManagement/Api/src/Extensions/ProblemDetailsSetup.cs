using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.WebUtilities;
using Tlis.Cms.UserManagement.Infrastructure.Exceptions;

namespace Tlis.Cms.UserManagement.Api.Extensions;

public static class ProblemDetailsSetup
{
    public static void ConfigureProblemDetails(this IServiceCollection services)
    {
        services.AddProblemDetails(options =>
        {
            options.CustomizeProblemDetails = context =>
            {
                var error = context.HttpContext.Features
                    .Get<IExceptionHandlerPathFeature>()?.Error;
                
                context.ProblemDetails.Status = error switch
                {
                    AuthProviderUserAlreadyExistsException _ => StatusCodes.Status409Conflict,
                    AuthProviderBadRequestException _ => StatusCodes.Status400BadRequest,
                    _ => context.ProblemDetails.Status
                };
                
                context.ProblemDetails.Title = error?.Message
                    ?? ReasonPhrases.GetReasonPhrase(context.ProblemDetails.Status!.Value);

                context.ProblemDetails.Type = context.ProblemDetails.Status switch
                {
                    StatusCodes.Status400BadRequest => "https://datatracker.ietf.org/doc/html/rfc7231#section-6.5.1",
                    StatusCodes.Status401Unauthorized => "https://datatracker.ietf.org/doc/html/rfc7235#section-3.1",
                    StatusCodes.Status404NotFound => "https://datatracker.ietf.org/doc/html/rfc7231#section-6.5.4",
                    StatusCodes.Status409Conflict => "https://datatracker.ietf.org/doc/html/rfc7231#section-6.5.8",
                    _ => "https://datatracker.ietf.org/doc/html/rfc7231#section-6.6.1"
                };
            };
        });   
    }
}
using System;
using Swashbuckle.AspNetCore.Annotations;

namespace Tlis.Cms.UserManagement.Application.Contracts.Api.Responses;

public class BaseCreateResponse
{
    [SwaggerSchema(Description = "The unique identifier of the newly created resource.")]
    public Guid Id { get; set; }
}
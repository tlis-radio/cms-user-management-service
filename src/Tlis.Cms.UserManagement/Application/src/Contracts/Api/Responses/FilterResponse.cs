using System.Collections.Generic;

namespace Tlis.Cms.UserManagement.Application.Contracts.Api.Responses;

public sealed class FilterResponse<T>
{
    public IReadOnlyCollection<T> Results { get; set; } = [];
}
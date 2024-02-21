using System.ComponentModel.DataAnnotations;
using MediatR;
using Tlis.Cms.UserManagement.Application.Contracts.Api.Responses;

namespace Tlis.Cms.UserManagement.Application.Contracts.Api.Requests;

public sealed class UserPaginationGetRequest : IRequest<PaginationResponse<UserPaginationGetResponse>>
{
    [Required]
    public int Limit { get; set; }

    [Required] [Range(1, int.MaxValue)]
    public int Page { get; set; }
}
using System.Collections.Immutable;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Tlis.Cms.UserManagement.Application.Contracts.Api.Requests;
using Tlis.Cms.UserManagement.Application.Contracts.Api.Responses;
using Tlis.Cms.UserManagement.Application.Mappers;
using Tlis.Cms.UserManagement.Infrastructure.Persistence.Interfaces;

namespace Tlis.Cms.UserManagement.Application.RequestHandlers;

internal sealed class UserPaginationGetRequestHandler(IUnitOfWork unitOfWork) 
    : IRequestHandler<UserPaginationGetRequest, PaginationResponse<UserPaginationGetResponse>>
{
    public async Task<PaginationResponse<UserPaginationGetResponse>> Handle(UserPaginationGetRequest request, CancellationToken cancellationToken)
    {
        var users = await unitOfWork.UserRepository.PaginationAsync(
            request.Limit,
            request.Page);

        return new PaginationResponse<UserPaginationGetResponse>
        {
            Total = users.Total,
            Limit = users.Limit,
            Page = users.Page,
            TotalPages = users.TotalPages,
            Results = users.Results.Select(UserMapper.ToPaginationDto).ToImmutableList()
        };
    }
}
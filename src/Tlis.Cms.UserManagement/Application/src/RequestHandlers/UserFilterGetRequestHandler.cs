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

internal sealed class UserFilterGetRequestHandler(IUnitOfWork unitOfWork) 
    : IRequestHandler<UserFilterGetRequest, FilterResponse<UserFilterGetResponse>>
{
    public async Task<FilterResponse<UserFilterGetResponse>> Handle(UserFilterGetRequest request, CancellationToken cancellationToken)
    {
        var users = await unitOfWork.UserRepository.FilterAsync(request.UserIds);

        return new FilterResponse<UserFilterGetResponse>
        {
            Results = users.Select(UserMapper.ToFilterDto).ToImmutableList()
        };
    }
}
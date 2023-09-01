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

internal sealed class UserPaginationGetRequestHandler : IRequestHandler<UserPaginationGetRequest, PaginationResponse<UserPaginationGetResponse>>
{
    private readonly IUnitOfWork _unitOfWork;

    private readonly UserMapper _mapper;

    public UserPaginationGetRequestHandler(IUnitOfWork unitOfWork, UserMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<PaginationResponse<UserPaginationGetResponse>> Handle(UserPaginationGetRequest request, CancellationToken cancellationToken)
    {
        var users = await _unitOfWork.UserRepository.PaginationAsync(
            request.Limit,
            request.Page,
            request.IsActive);

        return new PaginationResponse<UserPaginationGetResponse>
        {
            Total = users.Total,
            Limit = users.Limit,
            Page = users.Page,
            TotalPages = users.TotalPages,
            Results = users.Results.Select(_mapper.ToPaginationDto).ToImmutableList()
        };
    }
}
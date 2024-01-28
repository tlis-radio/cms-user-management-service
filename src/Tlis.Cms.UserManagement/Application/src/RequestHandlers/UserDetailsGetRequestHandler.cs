using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Tlis.Cms.UserManagement.Application.Contracts.Api.Requests;
using Tlis.Cms.UserManagement.Application.Contracts.Api.Responses;
using Tlis.Cms.UserManagement.Application.Mappers;
using Tlis.Cms.UserManagement.Infrastructure.Persistence.Interfaces;

namespace Tlis.Cms.UserManagement.Application.RequestHandlers;

internal sealed class UserDetailsGetRequestHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<UserDetailsGetRequest, UserDetailsGetResponse?>
{
    public async Task<UserDetailsGetResponse?> Handle(UserDetailsGetRequest request, CancellationToken cancellationToken)
    {
        var user = await unitOfWork.UserRepository.GetUserDetailsById(request.Id, asTracking: false);

        return UserMapper.ToDto(user);
    }
}
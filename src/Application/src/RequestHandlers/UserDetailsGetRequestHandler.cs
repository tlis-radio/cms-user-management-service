using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Tlis.Cms.UserManagement.Application.Contracts.Api.Requests;
using Tlis.Cms.UserManagement.Application.Contracts.Api.Responses;
using Tlis.Cms.UserManagement.Application.Mappers;
using Tlis.Cms.UserManagement.Infrastructure.Persistence.Interfaces;

namespace Tlis.Cms.UserManagement.Application.RequestHandlers;

internal sealed class UserDetailsGetRequestHandler : IRequestHandler<UserDetailsGetRequest, UserDetailsGetResponse?>
{
    private readonly IUnitOfWork _unitOfWork;

    private readonly UserMapper _userMapper;

    public UserDetailsGetRequestHandler(IUnitOfWork unitOfWork, UserMapper userMapper)
    {
        _unitOfWork = unitOfWork;
        _userMapper = userMapper;
    }

    public async Task<UserDetailsGetResponse?> Handle(UserDetailsGetRequest request, CancellationToken cancellationToken)
    {
        var user = await _unitOfWork.UserRepository.GetUserDetailsById(request.Id, asTracking: false);

        return _userMapper.ToDto(user);
    }
}
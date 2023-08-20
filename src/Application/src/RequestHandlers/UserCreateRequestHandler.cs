using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Tlis.Cms.UserManagement.Domain.Models;
using Tlis.Cms.UserManagement.Application.Contracts.Api.Requests;
using Tlis.Cms.UserManagement.Application.Contracts.Api.Responses;
using Tlis.Cms.UserManagement.Application.Exceptions;
using Tlis.Cms.UserManagement.Infrastructure.Persistence.Interfaces;
using Tlis.Cms.UserManagement.Infrastructure.Services.Interfaces;
using UserMapper = Tlis.Cms.UserManagement.Application.Mappers.UserMapper;

namespace Tlis.Cms.UserManagement.Application.RequestHandlers;

internal sealed class UserCreateRequestHandler : IRequestHandler<UserCreateRequest, BaseCreateResponse>
{
    private readonly IUnitOfWork _unitOfWork;

    private readonly UserMapper _userMapper;

    private readonly IAuthProviderManagementService _authProviderManagementService;

    private readonly IRoleService _roleService;

    public UserCreateRequestHandler(
        IAuthProviderManagementService authProviderManagementService,
        IUnitOfWork unitOfWork,
        IRoleService roleService,
        UserMapper userMapper)
    {
        _roleService = roleService;
        _unitOfWork = unitOfWork;
        _userMapper = userMapper;
        _authProviderManagementService = authProviderManagementService;
    }

    public async Task<BaseCreateResponse> Handle(UserCreateRequest request, CancellationToken cancellationToken)
    {
        var userToCreate = _userMapper.ToEntity(request);
        userToCreate.IsActive = true;

        var role = await _roleService.GetByIdAsync(request.RoleId) ?? throw new UserRoleNotFoundException(request.RoleId);

        userToCreate.RoleHistory = new[]
        {
            new UserRoleHistory
            {
                RoleForeignKey = role.Id,
                FunctionStartDate = request.FunctionStartDate
            }
        };

        if (!string.IsNullOrEmpty(request.Email) && !string.IsNullOrEmpty(request.Password))
            userToCreate.ExternalId = await _authProviderManagementService.CreateUser(
                request.Nickname,
                request.Email,
                request.Password
            );

        await _unitOfWork.UserRepository.InsertAsync(userToCreate);
        await _unitOfWork.SaveChangesAsync();

        return new BaseCreateResponse
        {
            Id = userToCreate.Id
        };
    }
}
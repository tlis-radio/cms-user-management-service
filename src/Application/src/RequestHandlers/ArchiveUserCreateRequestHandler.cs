using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Tlis.Cms.UserManagement.Domain.Entities;
using Tlis.Cms.UserManagement.Application.Contracts.Api.Requests;
using Tlis.Cms.UserManagement.Application.Contracts.Api.Responses;
using Tlis.Cms.UserManagement.Application.Exceptions;
using Tlis.Cms.UserManagement.Infrastructure.Persistence.Interfaces;
using Tlis.Cms.UserManagement.Infrastructure.Services.Interfaces;
using UserMapper = Tlis.Cms.UserManagement.Application.Mappers.UserMapper;

namespace Tlis.Cms.UserManagement.Application.RequestHandlers;

internal sealed class ArchiveUserCreateRequestHandler : IRequestHandler<ArchiveUserCreateRequest, BaseCreateResponse>
{
    private readonly IUnitOfWork _unitOfWork;

    private readonly UserMapper _userMapper;

    private readonly IRoleService _roleService;

    private readonly IAuthProviderManagementService _authProviderManagementService;

    public ArchiveUserCreateRequestHandler(
        IAuthProviderManagementService authProviderManagementService,
        IRoleService roleService,
        IUnitOfWork unitOfWork,
        UserMapper userMapper
    )
    {
        _authProviderManagementService = authProviderManagementService;
        _unitOfWork = unitOfWork;
        _userMapper = userMapper;
        _roleService = roleService;
    }

    public async Task<BaseCreateResponse> Handle(ArchiveUserCreateRequest request, CancellationToken cancellationToken)
    {
        var userToCreate = _userMapper.ToEntity(request);

        var roleHistory = new List<UserRoleHistory>();
        foreach (var history in request.RoleHistory)
        {
            var role = await _roleService.GetByIdAsync(history.RoleId) ?? throw new UserRoleNotFoundException(history.RoleId);

            roleHistory.Add(
                new UserRoleHistory
                {
                    RoleForeignKey = role.Id,
                    FunctionStartDate = history.FunctionStartDate,
                    FunctionEndDate = history.FunctionEndDate
                }
            );
        }
        userToCreate.RoleHistory = roleHistory;

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
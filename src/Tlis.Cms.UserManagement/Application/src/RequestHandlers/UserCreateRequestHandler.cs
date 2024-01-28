using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Tlis.Cms.UserManagement.Domain.Entities;
using Tlis.Cms.UserManagement.Application.Contracts.Api.Requests;
using Tlis.Cms.UserManagement.Application.Contracts.Api.Responses;
using Tlis.Cms.UserManagement.Application.Exceptions;
using Tlis.Cms.UserManagement.Infrastructure.Persistence.Interfaces;
using Tlis.Cms.UserManagement.Infrastructure.Services.Interfaces;
using Tlis.Cms.UserManagement.Application.Mappers;
using System;
using Tlis.Cms.UserManagement.Domain.Constants;

namespace Tlis.Cms.UserManagement.Application.RequestHandlers;

internal sealed class UserCreateRequestHandler(
    IAuthProviderManagementService authProviderManagementService,
    IUnitOfWork unitOfWork,
    IRoleService roleService) : IRequestHandler<UserCreateRequest, BaseCreateResponse>
{
    public async Task<BaseCreateResponse> Handle(UserCreateRequest request, CancellationToken cancellationToken)
    {
        var userToCreate = UserMapper.ToEntity(request);
        userToCreate.IsActive = true;

        var role = await roleService.GetByIdAsync(request.RoleId) ?? throw new UserRoleNotFoundException(request.RoleId);

        userToCreate.MembershipHistory = [
            new UserMembershipHistory
            {
                ChangeDate = DateTime.UtcNow,
                Status = MembershipStatus.Active
            }
        ];

        userToCreate.RoleHistory =
        [
            new UserRoleHistory
            {
                RoleId = role.Id,
                FunctionStartDate = request.FunctionStartDate
            }
        ];

        if (!string.IsNullOrEmpty(request.Email) && !string.IsNullOrEmpty(request.Password))
        {
            userToCreate.ExternalId = await authProviderManagementService.CreateUser(
                request.Nickname,
                request.Email,
                request.Password
            );
        }

        await unitOfWork.UserRepository.InsertAsync(userToCreate);
        await unitOfWork.SaveChangesAsync();

        return new BaseCreateResponse
        {
            Id = userToCreate.Id
        };
    }
}
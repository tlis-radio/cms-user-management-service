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
using System.Linq;

namespace Tlis.Cms.UserManagement.Application.RequestHandlers;

internal sealed class ArchiveUserCreateRequestHandler(
    IAuthProviderManagementService authProviderManagementService,
    IRoleService roleService,
    IUnitOfWork unitOfWork) : IRequestHandler<ArchiveUserCreateRequest, BaseCreateResponse>
{
    public async Task<BaseCreateResponse> Handle(ArchiveUserCreateRequest request, CancellationToken cancellationToken)
    {
        var userToCreate = UserMapper.ToEntity(request);

        userToCreate.MembershipHistory = request.MembershipHistory.Select(x => new UserMembershipHistory
        {
            Status = x.Status,
            ChangeDate = x.ChangeDate,
            Description = x.Description
        }).ToList();

        foreach (var history in request.RoleHistory)
        {
            var role = await roleService.GetByIdAsync(history.RoleId) ?? throw new UserRoleNotFoundException(history.RoleId);

            userToCreate.RoleHistory.Add(
                new UserRoleHistory
                {
                    RoleId = role.Id,
                    FunctionStartDate = history.FunctionStartDate,
                    FunctionEndDate = history.FunctionEndDate,
                    Description = history.Description
                }
            );
        }

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
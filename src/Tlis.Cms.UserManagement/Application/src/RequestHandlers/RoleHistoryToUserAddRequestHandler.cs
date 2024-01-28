using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Tlis.Cms.UserManagement.Application.Contracts.Api.Requests;
using Tlis.Cms.UserManagement.Application.Contracts.Api.Responses;
using Tlis.Cms.UserManagement.Application.Exceptions;
using Tlis.Cms.UserManagement.Domain.Entities;
using Tlis.Cms.UserManagement.Infrastructure.Persistence.Interfaces;
using Tlis.Cms.UserManagement.Infrastructure.Services.Interfaces;

namespace Tlis.Server.Application.RequestHandlers.UserController;

internal sealed class RoleHistoryToUserAddRequestHandler(IUnitOfWork unitOfWork, IRoleService roleService)
    : IRequestHandler<RoleHistoryToUserAddRequest, BaseCreateResponse?>
{
    public async Task<BaseCreateResponse?> Handle(
        RoleHistoryToUserAddRequest request,
        CancellationToken cancellationToken)
    {
        var user = await unitOfWork.UserRepository.GetUserWithRoleHistoriesById(request.Id, asTracking: true);
        if (user is null) return null;

        var role = await roleService.GetByIdAsync(request.RoleId) ?? throw new UserRoleNotFoundException(request.RoleId);

        user.RoleHistory.Add(new UserRoleHistory
        {
            RoleId = role.Id,
            FunctionStartDate = request.FunctionStartDate,
            FunctionEndDate = request.FunctionEndDate,
            Description = request.Description
        });

        await unitOfWork.SaveChangesAsync();

        return new BaseCreateResponse
        {
            Id = request.Id
        };
    }
}
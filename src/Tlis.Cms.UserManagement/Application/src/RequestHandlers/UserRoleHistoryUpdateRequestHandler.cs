using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Tlis.Cms.UserManagement.Application.Contracts.Api.Requests;
using Tlis.Cms.UserManagement.Application.Exceptions;
using Tlis.Cms.UserManagement.Infrastructure.Persistence.Interfaces;
using Tlis.Cms.UserManagement.Infrastructure.Services.Interfaces;

namespace Tlis.Cms.UserManagement.Application.RequestHandlers;

internal sealed class UserRoleHistoryUpdateRequestHandler(IUnitOfWork unitOfWork, IRoleService roleService)
    : IRequestHandler<UserRoleHistoryUpdateRequest, bool>
{
    public async Task<bool> Handle(UserRoleHistoryUpdateRequest request, CancellationToken cancellationToken)
    {
        var role = await roleService.GetByIdAsync(request.RoleId) ?? throw new UserRoleNotFoundException(request.RoleId);

        var userRoleHistory = await unitOfWork.UserRoleHistoryRepository.GetByIdAsync(request.HistoryId, asTracking: true);
        if (userRoleHistory is null || userRoleHistory.UserId != request.Id)
        {
            return false;
        }

        userRoleHistory.FunctionStartDate = request.FunctionStartDate;
        userRoleHistory.FunctionEndDate = request.FunctionEndDate;
        userRoleHistory.Description = request.Description;
        userRoleHistory.Role = role;

        await unitOfWork.SaveChangesAsync();

        return true;
    }
}
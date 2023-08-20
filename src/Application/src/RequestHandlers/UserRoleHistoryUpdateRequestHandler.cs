using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Tlis.Cms.UserManagement.Application.Contracts.Api.Requests;
using Tlis.Cms.UserManagement.Application.Exceptions;
using Tlis.Cms.UserManagement.Infrastructure.Persistence.Interfaces;
using Tlis.Cms.UserManagement.Infrastructure.Services.Interfaces;

namespace Tlis.Cms.UserManagement.Application.RequestHandlers;

internal sealed class UserRoleHistoryUpdateRequestHandler : IRequestHandler<UserRoleHistoryUpdateRequest, bool>
{
    private readonly IUnitOfWork _unitOfWork;

    private readonly IRoleService _roleService;

    public UserRoleHistoryUpdateRequestHandler(IUnitOfWork unitOfWork, IRoleService roleService)
    {
        _unitOfWork = unitOfWork;
        _roleService = roleService;
    }

    public async Task<bool> Handle(UserRoleHistoryUpdateRequest request, CancellationToken cancellationToken)
    {
        var role = await _roleService.GetByIdAsync(request.RoleId) ?? throw new UserRoleNotFoundException(request.RoleId);

        var userRoleHistory = await _unitOfWork.UserRoleHistoryRepository.GetByIdAsync(request.HistoryId, asTracking: true);
        if (userRoleHistory is null || userRoleHistory.UserForeignKey != request.Id)
        {
            return false;
        }

        userRoleHistory.FunctionStartDate = request.FunctionStartDate;
        userRoleHistory.FunctionEndDate = request.FunctionEndDate;
        userRoleHistory.Role = role;

        await _unitOfWork.SaveChangesAsync();

        return true;
    }
}
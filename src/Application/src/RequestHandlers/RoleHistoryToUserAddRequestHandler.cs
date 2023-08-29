using System.Collections.Generic;
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

internal sealed class RoleHistoryToUserAddRequestHandler : IRequestHandler<RoleHistoryToUserAddRequest, BaseCreateResponse?>
{
    private readonly IUnitOfWork _unitOfWork;

    private readonly IRoleService _roleService;

    public RoleHistoryToUserAddRequestHandler(IUnitOfWork unitOfWork, IRoleService roleService)
    {
        _unitOfWork = unitOfWork;
        _roleService = roleService;
    }

    public async Task<BaseCreateResponse?> Handle(
        RoleHistoryToUserAddRequest request,
        CancellationToken cancellationToken
    )
    {
        var user = await _unitOfWork.UserRepository.GetUserWithRoleHistoriesById(request.Id, asTracking: true);
        if (user is null) return null;

        var role = await _roleService.GetByIdAsync(request.RoleId) ?? throw new UserRoleNotFoundException(request.RoleId);

        user.RoleHistory ??= new List<UserRoleHistory>();

        user.RoleHistory.Add(new UserRoleHistory
        {
            RoleForeignKey = role.Id,
            FunctionStartDate = request.FunctionStartDate,
            FunctionEndDate = request.FunctionEndDate
        });

        await _unitOfWork.SaveChangesAsync();

        return new BaseCreateResponse
        {
            Id = request.Id
        };
    }
}
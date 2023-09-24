using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Tlis.Cms.UserManagement.Application.Contracts.Api.Requests;
using Tlis.Cms.UserManagement.Application.Contracts.Api.Responses;
using Tlis.Cms.UserManagement.Infrastructure.Persistence.Interfaces;
using Tlis.Cms.UserManagement.Infrastructure.Services.Interfaces;
using Tlis.Cms.UserManagement.Application.Mappers;
using System.Linq;

namespace Tlis.Cms.UserManagement.Application.RequestHandlers;

internal sealed class RoleGetAllRequestHandler : IRequestHandler<RoleGetAllRequest, RoleGetAllResponse>
{
    private readonly IUnitOfWork _unitOfWork;

    private readonly RoleMapper _roleMapper;

    private readonly IAuthProviderManagementService _authProviderManagementService;

    private readonly IRoleService _roleService;

    public RoleGetAllRequestHandler(
        IAuthProviderManagementService authProviderManagementService,
        IUnitOfWork unitOfWork,
        IRoleService roleService,
        RoleMapper roleMapper)
    {
        _roleService = roleService;
        _unitOfWork = unitOfWork;
        _roleMapper = roleMapper;
        _authProviderManagementService = authProviderManagementService;
    }

    public async Task<RoleGetAllResponse> Handle(RoleGetAllRequest request, CancellationToken cancellationToken)
    {
        var response = await _unitOfWork.RoleRepository.GetAll();

        return new RoleGetAllResponse
        {
            Results = response.Select(_roleMapper.ToRoleGetAllResponseItem).ToList()
        };
    }
}
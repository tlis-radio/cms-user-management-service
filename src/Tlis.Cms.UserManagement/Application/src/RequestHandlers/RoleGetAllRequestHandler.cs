using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Tlis.Cms.UserManagement.Application.Contracts.Api.Requests;
using Tlis.Cms.UserManagement.Application.Contracts.Api.Responses;
using Tlis.Cms.UserManagement.Infrastructure.Persistence.Interfaces;
using Tlis.Cms.UserManagement.Application.Mappers;
using System.Linq;

namespace Tlis.Cms.UserManagement.Application.RequestHandlers;

internal sealed class RoleGetAllRequestHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<RoleGetAllRequest, RoleGetAllResponse>
{
    public async Task<RoleGetAllResponse> Handle(RoleGetAllRequest request, CancellationToken cancellationToken)
    {
        var response = await unitOfWork.RoleRepository.GetAll();

        return new RoleGetAllResponse
        {
            Results = response.Select(RoleMapper.ToRoleGetAllResponseItem).ToList()
        };
    }
}
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Tlis.Cms.UserManagement.Application.Contracts.Api.Requests;
using Tlis.Cms.UserManagement.Application.Contracts.Api.Responses;
using Tlis.Cms.UserManagement.Infrastructure.Persistence.Interfaces;

namespace Tlis.Cms.UserManagement.Application.RequestHandlers;

internal sealed class MemebershipStatusGetAllRequestHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<MemebershipStatusGetAllRequest, MemebershipStatusGetAllResponse>
{
    public async Task<MemebershipStatusGetAllResponse> Handle(MemebershipStatusGetAllRequest request, CancellationToken cancellationToken)
    {
        var memberships = await unitOfWork.MembershipRepository.GetAll();

        return new MemebershipStatusGetAllResponse
        {
            Results = memberships.Select(membership => new MemebershipStatusGetAllResponseItem
            {
                Id = membership.Id,
                Name = Enum.GetName(membership.Status) ?? throw new Exception($"Unable to Enum.GetName for {membership.Status}")
            }).ToList()
        };
    }
}
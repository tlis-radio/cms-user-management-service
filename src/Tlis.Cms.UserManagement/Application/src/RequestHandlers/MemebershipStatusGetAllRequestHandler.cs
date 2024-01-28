using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Tlis.Cms.UserManagement.Application.Contracts.Api.Requests;
using Tlis.Cms.UserManagement.Application.Contracts.Api.Responses;
using Tlis.Cms.UserManagement.Domain.Constants;

namespace Tlis.Cms.UserManagement.Application.RequestHandlers;

internal sealed class MemebershipStatusGetAllRequestHandler
    : IRequestHandler<MemebershipStatusGetAllRequest, MemebershipStatusGetAllResponse>
{
    public Task<MemebershipStatusGetAllResponse> Handle(MemebershipStatusGetAllRequest request, CancellationToken cancellationToken)
    {
        return Task.FromResult(new MemebershipStatusGetAllResponse
        {
            Results = Enum.GetNames<MembershipStatus>().Select(name => new MemebershipStatusGetAllResponseItem
            {
                Name = name
            }).ToList()
        });
    }
}
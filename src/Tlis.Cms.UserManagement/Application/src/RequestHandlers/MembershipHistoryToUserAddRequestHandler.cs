using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Tlis.Cms.UserManagement.Application.Contracts.Api.Requests;
using Tlis.Cms.UserManagement.Application.Contracts.Api.Responses;
using Tlis.Cms.UserManagement.Application.Exceptions;
using Tlis.Cms.UserManagement.Domain.Entities;
using Tlis.Cms.UserManagement.Infrastructure.Persistence.Interfaces;

namespace Tlis.Cms.UserManagement.Application.RequestHandlers;

internal sealed class MembershipHistoryToUserAddRequestHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<MembershipHistoryToUserAddRequest, BaseCreateResponse?>
{
    public async Task<BaseCreateResponse?> Handle(MembershipHistoryToUserAddRequest request, CancellationToken cancellationToken)
    {
        var user = await unitOfWork.UserRepository.GetUserWithRoleHistoriesById(request.UserId, asTracking: true);
        if (user is null) return null;

        var membership = await unitOfWork.MembershipRepository.GetByIdAsync(request.MembershipId, asTracking: false);

        if (membership is null)
        {
            throw new MembershipNotFoundException(request.MembershipId);
        }

        user.MembershipHistory.Add(new UserMembershipHistory
        {
            MembershipId = membership.Id,
            Description = request.Description,
            ChangeDate = request.ChangeDate,
            UserId = request.UserId
        });

        await unitOfWork.SaveChangesAsync();

        return new BaseCreateResponse
        {
            Id = request.UserId
        };
    }
}
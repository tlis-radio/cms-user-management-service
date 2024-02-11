using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Tlis.Cms.UserManagement.Application.Contracts.Api.Requests;
using Tlis.Cms.UserManagement.Domain.Constants;
using Tlis.Cms.UserManagement.Domain.Entities;
using Tlis.Cms.UserManagement.Infrastructure.Persistence.Interfaces;

namespace Tlis.Cms.UserManagement.Application.RequestHandlers;

internal sealed class UserMembershipUpdateRequestHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<UserMembershipUpdateRequest, bool>
{
    public async Task<bool> Handle(UserMembershipUpdateRequest request, CancellationToken cancellationToken)
    {
        var user = await unitOfWork.UserRepository.GetByIdAsync(request.UserId, asTracking: true);
        var membership = await unitOfWork.MembershipRepository.GetByIdAsync(request.MembershipId, asTracking: false);

        if (user is null || membership is null)
        {
            return false;
        }

        user.IsActive = membership.Status switch
        {
            MembershipStatus.Active => true,
            MembershipStatus.Postponed => false,
            MembershipStatus.Archive => false,
            _ => throw new NotImplementedException()
        };

        await unitOfWork.UserMembershipHistoryRepository.InsertAsync(new UserMembershipHistory
        {
            UserId = request.UserId,
            ChangeDate = request.ChangeDate,
            Description = request.Description,
            MembershipId = membership.Id
        });
        await unitOfWork.SaveChangesAsync();

        return true;
    }
}
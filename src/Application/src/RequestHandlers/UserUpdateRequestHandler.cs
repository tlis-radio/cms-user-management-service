using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Tlis.Cms.UserManagement.Application.Contracts.Api.Requests;
using Tlis.Cms.UserManagement.Infrastructure.Persistence.Interfaces;

namespace Tlis.Cms.UserManagement.Application.RequestHandlers;

internal sealed class UserUpdateRequestHandler : IRequestHandler<UserUpdateRequest, bool>
{
    private readonly IUnitOfWork _unitOfWork;

    public UserUpdateRequestHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<bool> Handle(UserUpdateRequest request, CancellationToken cancellationToken)
    {
        var user = await _unitOfWork.UserRepository.GetByIdAsync(request.Id, asTracking: true);
        if (user is null) return false;

        user.Firstname = request.Firstname ?? user.Firstname;
        user.Lastname = request.Lastname ?? user.Lastname;
        user.Nickname = request.Nickname ?? user.Nickname;
        user.Description = request.Description ?? user.Description;
        user.IsActive = request.IsActive ?? user.IsActive;
        user.MemberSinceDate = request.MemberSinceDate ?? user.MemberSinceDate;
        user.MembershipEndedDate = request.MembershipEndedDate ?? user.MembershipEndedDate;
        user.Email = request.Email ?? user.Email;

        await _unitOfWork.SaveChangesAsync();

        return true;
    }
}
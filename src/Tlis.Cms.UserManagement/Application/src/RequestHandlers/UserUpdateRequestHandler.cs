using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Tlis.Cms.UserManagement.Application.Contracts.Api.Requests;
using Tlis.Cms.UserManagement.Infrastructure.Persistence.Interfaces;

namespace Tlis.Cms.UserManagement.Application.RequestHandlers;

internal sealed class UserUpdateRequestHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<UserUpdateRequest, bool>
{
    public async Task<bool> Handle(UserUpdateRequest request, CancellationToken cancellationToken)
    {
        var user = await unitOfWork.UserRepository.GetByIdAsync(request.Id, asTracking: true);
        if (user is null) return false;

        user.Firstname = request.Firstname ?? user.Firstname;
        user.Lastname = request.Lastname ?? user.Lastname;
        user.Nickname = request.Nickname ?? user.Nickname;
        user.Abouth = request.Description ?? user.Abouth;

        await unitOfWork.SaveChangesAsync();

        return true;
    }
}
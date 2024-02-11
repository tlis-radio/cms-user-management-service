using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Tlis.Cms.UserManagement.Application.Contracts.Api.Requests;
using Tlis.Cms.UserManagement.Infrastructure.Persistence.Interfaces;
using Tlis.Cms.UserManagement.Infrastructure.Services.Interfaces;

namespace Tlis.Cms.UserManagement.Application.RequestHandlers;

internal sealed class UserDeleteRequestHandler(
    IAuthProviderManagementService authProviderManagementService,
    IUnitOfWork unitOfWork)
    : IRequestHandler<UserDeleteRequest, bool>
{
    public async Task<bool> Handle(UserDeleteRequest request, CancellationToken cancellationToken)
    {
        var user = await unitOfWork.UserRepository.GetByIdAsync(request.Id, asTracking: false);
        if (user is null) return false;

        if (string.IsNullOrEmpty(user.ExternalId) is false)
        {
            await authProviderManagementService.DeleteUser(user.ExternalId);
        }

        await unitOfWork.UserRepository.DeleteByIdAsync(request.Id);
        await unitOfWork.SaveChangesAsync();

        return true;
    }
}
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Tlis.Cms.UserManagement.Application.Contracts.Api.Requests;
using Tlis.Cms.UserManagement.Infrastructure.Persistence.Interfaces;
using Tlis.Cms.UserManagement.Infrastructure.Services.Interfaces;

namespace Tlis.Cms.UserManagement.Application.RequestHandlers;

internal sealed class UserDeleteRequestHandler : IRequestHandler<UserDeleteRequest, bool>
{
    public IUnitOfWork _unitOfWork { get; set; }

    private readonly IAuthProviderManagementService _authProviderManagementService;

    private readonly IStorageService _storageService;

    public UserDeleteRequestHandler(
        IAuthProviderManagementService authProviderManagementService,
        IStorageService storageService,
        IUnitOfWork unitOfWork
    )
    {
        _authProviderManagementService = authProviderManagementService;
        _storageService = storageService;
        _unitOfWork = unitOfWork;
    }

    public async Task<bool> Handle(UserDeleteRequest request, CancellationToken cancellationToken)
    {
        var user = await _unitOfWork.UserRepository.GetByIdAsync(request.Id, asTracking: false);
        if (user is null) return false;

        if (string.IsNullOrEmpty(user.ExternalId) is false)
            await _authProviderManagementService.DeleteUser(user.ExternalId);

        await _unitOfWork.UserRepository.DeleteByIdAsync(request.Id);

        await _unitOfWork.SaveChangesAsync();
        if (string.IsNullOrEmpty(user.ProfileImageUrl) is false)
            await _storageService.DeleteUserProfileImage(user.ProfileImageUrl);

        return true;
    }
}
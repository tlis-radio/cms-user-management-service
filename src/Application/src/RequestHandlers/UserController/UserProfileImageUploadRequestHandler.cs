using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Tlis.Cms.UserManagement.Application.Contracts.Api.Requests;
using Tlis.Cms.UserManagement.Infrastructure.Persistence.Interfaces;
using Tlis.Cms.UserManagement.Infrastructure.Services.Interfaces;

namespace Tlis.Cms.UserManagement.Application.RequestHandlers.UserController;

internal sealed class UserProfileImageUploadRequestHandler : IRequestHandler<UserProfileImageUploadRequest, bool>
{
    private readonly IUnitOfWork _unitOfWork;

    private readonly IStorageService _storageService;

    public UserProfileImageUploadRequestHandler(
        IUnitOfWork unitOfWork,
        IStorageService storageService
    )
    {
        _unitOfWork = unitOfWork;
        _storageService = storageService;
    }

    public async Task<bool> Handle(UserProfileImageUploadRequest request, CancellationToken cancellationToken)
    {
        var user = await _unitOfWork.UserRepository.GetByIdAsync(request.Id, asTracking: true);
        if (user is null) return false;

        if (string.IsNullOrEmpty(user.ProfileImageUrl) is false)
            await _storageService.DeleteUserProfileImage(user.ProfileImageUrl);

        user.ProfileImageUrl = await _storageService.UploadUserProfileImage(request.Image);
        await _unitOfWork.SaveChangesAsync();

        return true;
    }
}
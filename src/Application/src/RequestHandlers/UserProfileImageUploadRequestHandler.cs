using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Tlis.Cms.UserManagement.Application.Contracts.Api.Requests;
using Tlis.Cms.UserManagement.Application.Contracts.Api.Responses;
using Tlis.Cms.UserManagement.Infrastructure.Persistence.Interfaces;
using Tlis.Cms.UserManagement.Infrastructure.Services.Interfaces;

namespace Tlis.Cms.UserManagement.Application.RequestHandlers;

internal sealed class UserProfileImageUploadRequestHandler : IRequestHandler<UserProfileImageUploadRequest, BaseCreateResponse?>
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

    public async Task<BaseCreateResponse?> Handle(UserProfileImageUploadRequest request, CancellationToken cancellationToken)
    {
        var user = await _unitOfWork.UserRepository.GetByIdAsync(request.Id, asTracking: true);
        if (user is null) return null;

        if (string.IsNullOrEmpty(user.ProfileImageUrl) is false)
            await _storageService.DeleteUserProfileImage(user.ProfileImageUrl);

        (var id, user.ProfileImageUrl) = await _storageService.UploadUserProfileImage(request.Image);
        await _unitOfWork.SaveChangesAsync();

        return new BaseCreateResponse { Id = id } ;
    }
}
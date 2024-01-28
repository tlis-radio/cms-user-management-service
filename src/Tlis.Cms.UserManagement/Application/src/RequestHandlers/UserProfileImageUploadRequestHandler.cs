using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Tlis.Cms.UserManagement.Application.Contracts.Api.Requests;
using Tlis.Cms.UserManagement.Application.Contracts.Api.Responses;
using Tlis.Cms.UserManagement.Infrastructure.Persistence.Interfaces;
using Tlis.Cms.UserManagement.Infrastructure.Services.Interfaces;

namespace Tlis.Cms.UserManagement.Application.RequestHandlers;

internal sealed class UserProfileImageUploadRequestHandler(
    IUnitOfWork unitOfWork,
    IStorageService storageService)
    : IRequestHandler<UserProfileImageUploadRequest, BaseCreateResponse?>
{
    public async Task<BaseCreateResponse?> Handle(UserProfileImageUploadRequest request, CancellationToken cancellationToken)
    {
        var user = await unitOfWork.UserRepository.GetByIdAsync(request.Id, asTracking: true);
        if (user is null) return null;

        if (string.IsNullOrEmpty(user.ProfileImageUrl) is false)
            await storageService.DeleteUserProfileImage(user.ProfileImageUrl);

        (var id, user.ProfileImageUrl) = await storageService.UploadUserProfileImage(request.Image);
        await unitOfWork.SaveChangesAsync();

        return new BaseCreateResponse { Id = id } ;
    }
}
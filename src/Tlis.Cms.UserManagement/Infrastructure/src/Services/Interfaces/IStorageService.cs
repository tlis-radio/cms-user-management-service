using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Tlis.Cms.UserManagement.Infrastructure.Services.Interfaces;

public interface IStorageService
{
    public Task<bool> DeleteUserProfileImage(string fileUrl);

    public Task<(Guid, string)> UploadUserProfileImage(IFormFile file);
}
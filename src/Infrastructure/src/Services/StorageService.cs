using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Tlis.Cms.UserManagement.Infrastructure.Configurations;
using Tlis.Cms.UserManagement.Infrastructure.Services.Interfaces;

namespace Tlis.Cms.UserManagement.Infrastructure.Services;

internal sealed class StorageService : IStorageService
{
    private const string UserProfileImagesContainer = "user-profile-images";
    
    private readonly string _storageAccountUrl;

    private readonly BlobContainerClient _userProfileImagesContainerClient;
    
    private readonly ILogger<StorageService> _logger;

    public StorageService(
        IConfiguration configuration,
        ILogger<StorageService> logger,
        IOptions<ServiceUrlsConfiguration> serviceUrlsConfiguration)
    {
        _logger = logger;
        _storageAccountUrl = serviceUrlsConfiguration.Value.StorageAccount;
        _userProfileImagesContainerClient = new BlobContainerClient(
            configuration.GetConnectionString("StorageAccount"),
            UserProfileImagesContainer);
    }
    
    public Task<bool> DeleteUserProfileImage(string fileUrl)
        => DeleteFile(_userProfileImagesContainerClient, fileUrl);


    public Task<(Guid, string)> UploadUserProfileImage(IFormFile file)
        => UploadFile(_userProfileImagesContainerClient, file, UserProfileImagesContainer);
    
    private async Task<bool> DeleteFile(BlobContainerClient client, string fileUrl)
    {
        try
        {
            var response = await client.DeleteBlobAsync(fileUrl.Split('/').Last());

            return response.Status == 202;
        }
        catch (Exception exception)
        {
            _logger.LogError(exception.Message);
            return false;
        }
    }

    private Task<(Guid, string)> UploadFile(BlobContainerClient client, IFormFile file, string containerName, bool overrideFileName = true)
        => UploadFile(client, file.OpenReadStream(), file.FileName, file.ContentType, containerName, overrideFileName);

    private async Task<(Guid, string)> UploadFile(
        BlobContainerClient client,
        Stream stream,
        string fileName,
        string contentType,
        string containerName,
        bool overrideFileName = true)
    {
        var guid = Guid.NewGuid();
        var storageFileName = overrideFileName ? GetStorageFileName(guid, fileName) : fileName;
        var blob = client.GetBlobClient(storageFileName);

        await blob.UploadAsync(stream, new BlobUploadOptions
        {
            HttpHeaders = new BlobHttpHeaders
            {
                ContentType = contentType
            }
        });

        return (guid, Path.Combine(_storageAccountUrl, containerName, storageFileName));
    }

    private string GetStorageFileName(Guid guid, string fileName) => $"{guid}{Path.GetExtension(fileName)}";
}
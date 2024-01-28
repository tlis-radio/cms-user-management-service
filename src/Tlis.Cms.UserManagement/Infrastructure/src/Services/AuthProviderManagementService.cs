using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Auth0.Core.Exceptions;
using Auth0.ManagementApi;
using Auth0.ManagementApi.Models;
using Microsoft.Extensions.Options;
using Tlis.Cms.UserManagement.Infrastructure.Configurations;
using Tlis.Cms.UserManagement.Infrastructure.Exceptions;
using Tlis.Cms.UserManagement.Infrastructure.Services.Interfaces;

namespace Tlis.Cms.UserManagement.Infrastructure.Services;

internal sealed class AuthProviderManagementService(
    ITokenProviderService tokenProviderService,
    HttpClient httpClient,
    IOptions<Auth0Configuration> configuration)
    : IAuthProviderManagementService
{
    private readonly IManagementConnection _managementConnection = new HttpClientManagementConnection(httpClient);

    private readonly string _domain = configuration.Value.Domain;

    public async ValueTask<string> CreateUser(string username, string email, string password)
    {
        try
        {
            using var client = await GetApiClient();

            var response = await client.Users.CreateAsync(
                new UserCreateRequest
                {
                    Email = email,
                    UserName = username,
                    Password = password,
                    Connection = "Username-Password-Authentication"
                }
            );

            return response.UserId;
        }
        catch (ErrorApiException ex)
        {
            throw ex.StatusCode switch
            {
                HttpStatusCode.Conflict => new AuthProviderUserAlreadyExistsException(ex.Message),
                HttpStatusCode.BadRequest => new AuthProviderBadRequestException(ex.Message),
                _ => new AuthProviderException(ex.Message)
            };
        }
    }

    public async Task DeleteUser(string id)
    {
        using var client = await GetApiClient();

        await client.Users.DeleteAsync(id);
    }

    private async ValueTask<IManagementApiClient> GetApiClient() =>
        new ManagementApiClient(await tokenProviderService.GetAuth0AccessToken(), _domain, _managementConnection);
}
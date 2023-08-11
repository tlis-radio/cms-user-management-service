using System.Net.Http;
using System.Threading.Tasks;
using Auth0.ManagementApi;
using Auth0.ManagementApi.Models;
using Microsoft.Extensions.Options;
using Tlis.Cms.UserManagement.Infrastructure.Configurations;
using Tlis.Cms.UserManagement.Infrastructure.Services.Interfaces;

namespace Tlis.Cms.UserManagement.Infrastructure.Services;

internal sealed class AuthProviderManagementService : IAuthProviderManagementService
{
    private readonly IManagementConnection _managementConnection;

    private readonly ITokenProviderService _tokenProviderService;

    private readonly string _domain;

    public AuthProviderManagementService(
        ITokenProviderService tokenProviderService,
        HttpClient httpClient,
        IOptions<Auth0Configuration> configuration)
    {
        _domain = configuration.Value.Domain;
        _tokenProviderService = tokenProviderService;
        _managementConnection = new HttpClientManagementConnection(httpClient);
    }

    public async ValueTask<string> CreateUser(string username, string email, string password)
    {
        var client = await GetApiClient();
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

    public async Task DeleteUser(string id)
    {
        var client = await GetApiClient();

        await client.Users.DeleteAsync(id);
    }

    private async ValueTask<IManagementApiClient> GetApiClient() =>
        new ManagementApiClient(await _tokenProviderService.GetAuth0AccessToken(), _domain, _managementConnection);
}
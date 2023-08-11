using System;
using System.Net.Http;
using System.Threading.Tasks;
using Auth0.AuthenticationApi;
using Auth0.AuthenticationApi.Models;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using Tlis.Cms.UserManagement.Infrastructure.Configurations;
using Tlis.Cms.UserManagement.Infrastructure.Services.Interfaces;

namespace Tlis.Cms.UserManagement.Infrastructure.Services;

internal sealed class TokenProviderService : ITokenProviderService
{
    private readonly IAuthenticationConnection _connection;

    private readonly Auth0Configuration _configuration;

    private readonly IMemoryCache _memoryCache;

    private const string Auth0AccessTokenKey = "AUTH0_ACCESS_TOKEN_KEY";

    public TokenProviderService(
        HttpClient httpClient,
        IOptions<Auth0Configuration> configuration,
        IMemoryCache memoryCache)
    {
        _configuration = configuration.Value;
        _memoryCache = memoryCache;
        _connection = new HttpClientAuthenticationConnection(httpClient);
    }

    public async ValueTask<string> GetAuth0AccessToken(bool force = false)
    {
        if (force is false && _memoryCache.TryGetValue<string>(Auth0AccessTokenKey, out var accessToken))
        {
            if (accessToken is not null)
                return accessToken;
        }

        var client = new AuthenticationApiClient(_configuration.Domain, _connection);
        var response = await client.GetTokenAsync(new ClientCredentialsTokenRequest
        {
            Audience = $"https://{_configuration.Domain}/api/v2/",
            ClientId = _configuration.ClientId,
            ClientSecret = _configuration.ClientSecret
        });

        _memoryCache.Set(
            Auth0AccessTokenKey,
            response.AccessToken,
            TimeSpan.FromSeconds(response.ExpiresIn));

        return response.AccessToken;
    }
}
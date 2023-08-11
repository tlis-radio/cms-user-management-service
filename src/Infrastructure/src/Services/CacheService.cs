using Microsoft.Extensions.Caching.Memory;
using Tlis.Cms.UserManagement.Infrastructure.Services.Interfaces;

namespace Tlis.Cms.UserManagement.Infrastructure.Services;

internal sealed class CacheService : ICacheService
{
    private readonly IMemoryCache _cache;

    public CacheService(IMemoryCache cache)
    {
        _cache = cache;
    }

    public bool TryGetValue<T>(string key, out T? value)
    {
        return _cache.TryGetValue(key, out value);
    }

    public void Set<T>(string key, T value)
    {
        _cache.Set(key, value);
    }
}
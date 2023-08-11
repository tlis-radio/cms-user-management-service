namespace Tlis.Cms.UserManagement.Infrastructure.Services.Interfaces;

public interface ICacheService
{
    public bool TryGetValue<T>(string key, out T? value);

    public void Set<T>(string key, T value);
}
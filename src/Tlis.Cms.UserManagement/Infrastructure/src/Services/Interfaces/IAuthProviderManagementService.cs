using System.Threading.Tasks;

namespace Tlis.Cms.UserManagement.Infrastructure.Services.Interfaces;

public interface IAuthProviderManagementService
{
    public ValueTask<string> CreateUser(string username, string email, string password);

    public Task DeleteUser(string id);
}
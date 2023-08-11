using System.Threading.Tasks;
using Tlis.Cms.UserManagement.Infrastructure.Exceptions;
using Tlis.Cms.UserManagement.Infrastructure.Persistence.Repositories.Interfaces;

namespace Tlis.Cms.UserManagement.Infrastructure.Persistence.Interfaces;

public interface IUnitOfWork
{
    public IUserRepository UserRepository { get; }

    public IRoleRepository RoleRepository { get; }

    public IUserRoleHistoryRepository UserRoleHistoryRepository { get; }

    void SetStateUnchanged<TEntity>(params TEntity[] entities) where TEntity : class;

    /// <exception cref="EntityAlreadyExistsException">Thrown when a unique constraint is violated</exception>
    /// <exception cref="ApiException">Thrown when an error occurs while saving changes</exception>
    public Task SaveChangesAsync();

    public Task ExecutePendingMigrationsAsync();
}
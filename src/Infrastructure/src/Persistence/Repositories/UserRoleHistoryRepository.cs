using Tlis.Cms.UserManagement.Domain.Entities;
using Tlis.Cms.UserManagement.Infrastructure.Persistence.Repositories.Interfaces;

namespace Tlis.Cms.UserManagement.Infrastructure.Persistence.Repositories;

internal sealed class UserRoleHistoryRepository : GenericRepository<UserRoleHistory>, IUserRoleHistoryRepository
{
    public UserRoleHistoryRepository(UserManagementDbContext context) : base(context)
    {
    }
}
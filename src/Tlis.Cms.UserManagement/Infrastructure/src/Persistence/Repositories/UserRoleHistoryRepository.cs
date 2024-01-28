using Tlis.Cms.UserManagement.Domain.Entities;
using Tlis.Cms.UserManagement.Infrastructure.Persistence.Repositories.Interfaces;

namespace Tlis.Cms.UserManagement.Infrastructure.Persistence.Repositories;

internal sealed class UserRoleHistoryRepository(UserManagementDbContext context)
    : GenericRepository<UserRoleHistory>(context), IUserRoleHistoryRepository
{
}
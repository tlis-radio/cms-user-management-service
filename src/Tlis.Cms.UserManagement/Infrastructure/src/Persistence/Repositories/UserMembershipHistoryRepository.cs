using Tlis.Cms.UserManagement.Domain.Entities;
using Tlis.Cms.UserManagement.Infrastructure.Persistence.Repositories.Interfaces;

namespace Tlis.Cms.UserManagement.Infrastructure.Persistence.Repositories;

internal sealed class UserMembershipHistoryRepository(UserManagementDbContext context)
    : GenericRepository<UserMembershipHistory>(context), IUserMembershipHistoryRepository
{
}
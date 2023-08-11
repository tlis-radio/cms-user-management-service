using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Tlis.Cms.UserManagement.Infrastructure.Persistence.Repositories.Interfaces;

namespace Tlis.Cms.UserManagement.Infrastructure.Persistence.Repositories;

internal sealed class UserRepository : GenericRepository<Domain.Models.User>, IUserRepository
{
    public UserRepository(UserManagementDbContext context) : base(context)
    {
    }

    public Task<Domain.Models.User?> GetUserWithRoleHistoriesById(Guid id, bool asTracking)
    {
        var query = ConfigureTracking(dbSet.AsQueryable(), asTracking);

        query.Include(u => u.RoleHistory);

        return query.FirstOrDefaultAsync(u => u.Id == id);
    }
}
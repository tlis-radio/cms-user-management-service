using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Tlis.Cms.UserManagement.Domain.Models;
using Tlis.Cms.UserManagement.Infrastructure.Persistence.Repositories.Interfaces;

namespace Tlis.Cms.UserManagement.Infrastructure.Persistence.Repositories;

internal sealed class UserRepository : GenericRepository<User>, IUserRepository
{
    public UserRepository(UserManagementDbContext context) : base(context)
    {
    }

    public Task<User?> GetUserWithRoleHistoriesById(Guid id, bool asTracking)
    {
        var query = ConfigureTracking(dbSet.AsQueryable(), asTracking);

        query.Include(u => u.RoleHistory);

        return query.FirstOrDefaultAsync(u => u.Id == id);
    }

    public Task<User?> GetUserDetailsById(Guid id, bool asTracking)
    {
        var query = ConfigureTracking(dbSet.AsQueryable(), asTracking);

        query.Include(u => u.RoleHistory)
            !.ThenInclude(rh => rh.Role);

        return query.FirstOrDefaultAsync(u => u.Id == id);
    }
}
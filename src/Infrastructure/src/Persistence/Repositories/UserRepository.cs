using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Tlis.Cms.UserManagement.Domain.Entities;
using Tlis.Cms.UserManagement.Infrastructure.Persistence.Dtos;
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

        return query
            .Include(u => u.RoleHistory)
            .FirstOrDefaultAsync(u => u.Id == id);
    }

    public Task<User?> GetUserDetailsById(Guid id, bool asTracking)
    {
        var query = ConfigureTracking(dbSet.AsQueryable(), asTracking);

        return query
            .Include(u => u.RoleHistory)
                !.ThenInclude(rh => rh.Role)
            .FirstOrDefaultAsync(u => u.Id == id);
    }
    
    public async Task< PaginationDto<User>> PaginationAsync(int limit, int pageNumber, bool isActive)
    {
        var queryGetTotalCount = await ConfigureTracking(dbSet.AsQueryable(), false)
            .Where(x => x.IsActive == isActive).CountAsync();
        
        var pageQuery = ConfigureTracking(dbSet.AsQueryable(), false)
            .Where(x => x.IsActive == isActive);

        var page = await pageQuery
            .Include(u => u.RoleHistory)
            !.ThenInclude(rh => rh.Role)
            .OrderBy(u => u.Nickname)
            .Skip(limit * (pageNumber - 1))
            .Take(limit)
            .ToListAsync();
        
        return new PaginationDto<User>
        {
            Total = queryGetTotalCount,
            Limit = limit,
            Page = pageNumber,
            Results = page
        };
    }
}
using System;
using System.Collections.Generic;
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

    public async Task<IList<UserWithOnlyNicknameDto>> GetUsersWithOnlyNickName(IEnumerable<Guid> ids)
    {
        var query = ConfigureTracking(DbSet.AsQueryable(), false);

        return await query
            .Where(u => ids.Contains(u.Id))
            .Select(u => new UserWithOnlyNicknameDto
                {
                    Id = u.Id,
                    Nickname = u.Nickname
                })
            .ToListAsync();
    }

    public Task<User?> GetUserWithRoleHistoriesById(Guid id, bool asTracking)
    {
        var query = ConfigureTracking(DbSet.AsQueryable(), asTracking);

        return query
            .Include(u => u.RoleHistory)
            .FirstOrDefaultAsync(u => u.Id == id);
    }

    public Task<User?> GetUserDetailsById(Guid id, bool asTracking)
    {
        var query = ConfigureTracking(DbSet.AsQueryable(), asTracking);

        return query
            .Include(u => u.RoleHistory)
                !.ThenInclude(rh => rh.Role)
            .FirstOrDefaultAsync(u => u.Id == id);
    }
    
    public async Task< PaginationDto<User>> PaginationAsync(int limit, int pageNumber, bool isActive)
    {
        var queryGetTotalCount = await ConfigureTracking(DbSet.AsQueryable(), false)
            .Where(x => x.IsActive == isActive).CountAsync();
        
        var pageQuery = ConfigureTracking(DbSet.AsQueryable(), false)
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
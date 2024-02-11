using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Tlis.Cms.UserManagement.Domain.Constants;
using Tlis.Cms.UserManagement.Domain.Entities;
using Tlis.Cms.UserManagement.Infrastructure.Persistence.Repositories.Interfaces;

namespace Tlis.Cms.UserManagement.Infrastructure.Persistence.Repositories;

internal sealed class MembershipRepository(UserManagementDbContext context)
    : GenericRepository<Membership>(context), IMembershipRepository
{
    public Task<Guid?> GetIdByStatus(MembershipStatus status)
        => GetIdAsync(x => x.Status == status);

    public Task<List<Membership>> GetAll()
    {
        var query = ConfigureTracking(DbSet.AsQueryable(), false);

        return query.ToListAsync();
    }
}
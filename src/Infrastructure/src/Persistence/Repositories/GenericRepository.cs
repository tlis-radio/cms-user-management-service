using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Tlis.Cms.UserManagement.Domain.Models.Base;
using Tlis.Cms.UserManagement.Infrastructure.Persistence.Repositories.Interfaces;

namespace Tlis.Cms.UserManagement.Infrastructure.Persistence.Repositories;

internal abstract class GenericRepository<TEntity> : IGenericRepository<TEntity>
    where TEntity : BaseEntity
{
    protected readonly DbSet<TEntity> dbSet;

    protected readonly UserManagementDbContext context;

    public GenericRepository(UserManagementDbContext context)
    {
        dbSet = context.Set<TEntity>();
        this.context = context;
    }

    public Task<TEntity?> GetByIdAsync(Guid id, bool asTracking)
    {
        var query = ConfigureTracking(dbSet.AsQueryable(), asTracking);

        return query.FirstOrDefaultAsync(x => x.Id == id);
    }

    public Task<List<TEntity>> GetByIdsAsync(IEnumerable<Guid> ids, bool asTracking)
    {
        var query = ConfigureTracking(dbSet.AsQueryable(), asTracking);

        return query.Where(x => ids.Contains(x.Id)).ToListAsync();
    }

    public async Task<List<TEntity>> GetAsync(Expression<Func<TEntity, bool>> predicate, bool asTracking)
    {
        var query = ConfigureTracking(dbSet.AsQueryable(), asTracking);

        return await query.Where(predicate).ToListAsync();
    }

    public async Task InsertAsync(TEntity entity)
    {
        await dbSet.AddAsync(entity);
    }

    public async Task InsertRangeAsync(IEnumerable<TEntity> entities)
    {
        await dbSet.AddRangeAsync(entities);
    }

    public void Update(TEntity entity)
    {
        dbSet.Update(entity);
    }

    public async ValueTask<bool> DeleteByIdAsync(Guid id)
    {
        var toDelete = await dbSet.FindAsync(id);
        if (toDelete is null)
            return false;

        dbSet.Remove(toDelete);
        return true;
    }

    public bool Delete(TEntity toDelete)
    {
        dbSet.Remove(toDelete);
        return true;
    }

    public bool Delete(IEnumerable<TEntity>? toDelete)
    {
        if (toDelete is null)
            return true;

        dbSet.RemoveRange(toDelete);
        return true;
    }

    protected IQueryable<TEntity> ConfigureTracking(IQueryable<TEntity> query, bool asTracking)
    {
        if (asTracking)
        {
            return query.AsTracking();
        }

        return query.AsNoTracking();
    }
}
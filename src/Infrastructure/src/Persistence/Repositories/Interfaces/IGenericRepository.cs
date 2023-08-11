using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Tlis.Cms.UserManagement.Domain.Models.Base;

namespace Tlis.Cms.UserManagement.Infrastructure.Persistence.Repositories.Interfaces;

public interface IGenericRepository<TEntity> where TEntity : BaseEntity
{
    public Task<TEntity?> GetByIdAsync(Guid id, bool asTracking);

    public Task<List<TEntity>> GetByIdsAsync(IEnumerable<Guid> ids, bool asTracking);

    public Task<List<TEntity>> GetAsync(Expression<Func<TEntity, bool>> predicate, bool asTracking);

    public Task InsertAsync(TEntity entity);

    public Task InsertRangeAsync(IEnumerable<TEntity> entities);

    void Update(TEntity entityToUpdate);

    public ValueTask<bool> DeleteByIdAsync(int id);

    public bool Delete(TEntity toDelete);

    public bool Delete(IEnumerable<TEntity>? toDelete);
}
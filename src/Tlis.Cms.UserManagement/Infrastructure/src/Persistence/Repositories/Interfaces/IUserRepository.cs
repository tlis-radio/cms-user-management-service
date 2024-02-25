using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Tlis.Cms.UserManagement.Domain.Entities;
using Tlis.Cms.UserManagement.Infrastructure.Persistence.Dtos;

namespace Tlis.Cms.UserManagement.Infrastructure.Persistence.Repositories.Interfaces;

public interface IUserRepository : IGenericRepository<User>
{
    Task<User?> GetUserWithRoleHistoriesById(Guid id, bool asTracking);

    Task<User?> GetUserDetailsById(Guid id, bool asTracking);

    Task<PaginationDto<User>> PaginationAsync(int limit, int page);

    Task<List<User>> FilterAsync(List<Guid> ids);
}
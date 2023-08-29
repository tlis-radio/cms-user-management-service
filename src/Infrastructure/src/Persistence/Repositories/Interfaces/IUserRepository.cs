using System;
using System.Threading.Tasks;
using Tlis.Cms.UserManagement.Domain.Entities;

namespace Tlis.Cms.UserManagement.Infrastructure.Persistence.Repositories.Interfaces;

public interface IUserRepository : IGenericRepository<User>
{
    Task<User?> GetUserWithRoleHistoriesById(Guid id, bool asTracking);

    Task<User?> GetUserDetailsById(Guid id, bool asTracking);
}
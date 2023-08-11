using System;
using System.Threading.Tasks;
using Tlis.Cms.UserManagement.Domain.Models;

namespace Tlis.Cms.UserManagement.Infrastructure.Persistence.Repositories.Interfaces;

public interface IUserRepository : IGenericRepository<Domain.Models.User>
{
    Task<Domain.Models.User?> GetUserWithRoleHistoriesById(Guid id, bool asTracking);
}
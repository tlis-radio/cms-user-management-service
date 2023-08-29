using System.Threading.Tasks;
using Tlis.Cms.UserManagement.Domain.Entities;

namespace Tlis.Cms.UserManagement.Infrastructure.Persistence.Repositories.Interfaces;

public interface IRoleRepository : IGenericRepository<Role>
{
    public Task<Role?> GetByName(string name, bool asTracking);
}
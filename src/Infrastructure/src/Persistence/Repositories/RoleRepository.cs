using System.Linq;
using System.Threading.Tasks;
using Tlis.Cms.UserManagement.Domain.Models;
using Tlis.Cms.UserManagement.Infrastructure.Persistence.Repositories.Interfaces;

namespace Tlis.Cms.UserManagement.Infrastructure.Persistence.Repositories;

internal sealed class RoleRepository : GenericRepository<Role>, IRoleRepository
{
    public RoleRepository(UserManagementDbContext context) : base(context)
    {
    }

    public async Task<Role?> GetByName(string name, bool asTracking)
        => (await GetAsync(x => x.Name == name, asTracking)).FirstOrDefault();
}
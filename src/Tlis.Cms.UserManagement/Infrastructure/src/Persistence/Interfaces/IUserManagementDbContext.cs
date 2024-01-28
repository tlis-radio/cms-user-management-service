using Microsoft.EntityFrameworkCore.Infrastructure;

namespace Tlis.Cms.UserManagement.Infrastructure.Persistence.Interfaces;

public interface IUserManagementDbContext
{
    DatabaseFacade Database { get; }
}
using Microsoft.EntityFrameworkCore;
using Tlis.Cms.UserManagement.Domain.Models;

namespace Tlis.Cms.UserManagement.Infrastructure.Persistence.Interfaces;

public interface IUserManagementDbContext
{
    public DbSet<Role> Role { get; set; }

    public DbSet<User> User { get; set; }

    public int SaveChanges();
}
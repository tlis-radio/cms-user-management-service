using System;
using EntityFramework.Exceptions.PostgreSQL;
using Microsoft.EntityFrameworkCore;
using Tlis.Cms.UserManagement.Domain.Models;
using Tlis.Cms.UserManagement.Infrastructure.Persistence.Interfaces;

namespace Tlis.Cms.UserManagement.Infrastructure.Persistence;

public class UserManagementDbContext : DbContext, IUserManagementDbContext
{
    public DbSet<Role> Role { get; set; } = null!;
    public DbSet<Domain.Models.User> User { get; set; } = null!;
    
    public UserManagementDbContext(DbContextOptions options)
        : base(options)
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseExceptionProcessor();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(UserManagementDbContext).Assembly);
        modelBuilder.HasDefaultSchema("cms_user_management");
        base.OnModelCreating(modelBuilder);
    }
}
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.ValueGeneration;
using Tlis.Cms.UserManagement.Domain.Constants;
using Tlis.Cms.UserManagement.Domain.Models;

namespace Tlis.Cms.UserManagement.Infrastructure.Persistence.Configurations;

public class RoleEntityConfiguration : IEntityTypeConfiguration<Role>
{
    public void Configure(EntityTypeBuilder<Role> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).ValueGeneratedOnAdd().HasValueGenerator((_, _) => new GuidValueGenerator());

        builder
            .Property(x => x.Id)
            .IsRequired()
            .ValueGeneratedOnAdd();

        builder
            .Property(x => x.Name)
            .IsRequired();

        builder
            .HasMany(x => x.UserRoleHistory)
            .WithOne(x => x.Role)
            .HasForeignKey(x => x.RoleForeignKey);

        builder.HasData(new List<Role>()
        {
            new () { Id = Guid.NewGuid(), Name = UserRole.SystemAdmin },
            new () { Id = Guid.NewGuid(), Name = UserRole.Technician },
            new () { Id = Guid.NewGuid(), Name = UserRole.Moderator }
        });
    }
}
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.ValueGeneration;
using Tlis.Cms.UserManagement.Domain.Constants;
using Tlis.Cms.UserManagement.Domain.Entities;

namespace Tlis.Cms.UserManagement.Infrastructure.Persistence.Configurations;

public class RoleEntityConfiguration : IEntityTypeConfiguration<Role>
{
    public void Configure(EntityTypeBuilder<Role> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).ValueGeneratedOnAdd().HasValueGenerator((_, _) => new GuidValueGenerator());

        builder.Property(x => x.Name).IsRequired();

        builder.HasIndex(x => x.Id);

        builder.HasData(new List<Role>()
        {
            new ()
            {
                Id = Guid.Parse("a9a9040c-fbbd-4aa6-b0dc-56de7265ee7f"),
                Name = UserRole.SystemAdmin
            },
            new ()
            {
                Id = Guid.Parse("cbec6f46-a2e8-4fb3-a126-fe4e51e5ead2"),
                Name = UserRole.Technician
            },
            new ()
            {
                Id = Guid.Parse("ed7cafb5-f2bf-4fbe-972c-18fa4f056b69"),
                Name = UserRole.Moderator
            },
            new ()
            {
                Id = Guid.Parse("fab1118e-38b9-4164-b222-66378654fcf4"),
                Name = UserRole.Graphic
            },
            new ()
            {
                Id = Guid.Parse("8570d900-396f-4b78-bf69-5065e2fe8acf"),
                Name = UserRole.MarketingPr
            },
            new ()
            {
                Id = Guid.Parse("4971ba3e-5a40-42cf-b9d9-17c49d9da309"),
                Name = UserRole.DramaturgeDj
            },
            new ()
            {
                Id = Guid.Parse("f5bdf1df-8406-44d6-b1a1-942f7bde7b23"),
                Name = UserRole.WebDeveloper
            }
        });
    }
}
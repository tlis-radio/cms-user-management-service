using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.ValueGeneration;
using Tlis.Cms.UserManagement.Domain.Constants;
using Tlis.Cms.UserManagement.Domain.Entities;

namespace Tlis.Cms.UserManagement.Infrastructure.Persistence.Configurations;

public class MembershipEntityConfiguration : IEntityTypeConfiguration<Membership>
{
    public void Configure(EntityTypeBuilder<Membership> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).ValueGeneratedOnAdd().HasValueGenerator((_, _) => new GuidValueGenerator());

        builder.Property(x => x.Status).HasConversion<string>().IsRequired();

        builder.HasIndex(x => x.Id);

        builder.HasData(new List<Membership>()
        {
            new () { Id = Guid.NewGuid(), Status = MembershipStatus.Active },
            new () { Id = Guid.NewGuid(), Status = MembershipStatus.Archive },
            new () { Id = Guid.NewGuid(), Status = MembershipStatus.Postponed }
        });
    }
}
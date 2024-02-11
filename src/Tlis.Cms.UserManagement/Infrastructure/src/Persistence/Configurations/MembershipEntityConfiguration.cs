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
            new ()
            {
                Id = Guid.Parse("80126b05-9dab-4709-aa6a-39baa5bafe79"),
                Status = MembershipStatus.Active
            },
            new ()
            {
                Id = Guid.Parse("a7c0bea2-2812-40b6-9836-d4b5accae95a"),
                Status = MembershipStatus.Archive
            },
            new ()
            {
                Id = Guid.Parse("cfaeecff-d26b-44f2-bfa1-c80ab79983a9"),
                Status = MembershipStatus.Postponed
            }
        });
    }
}
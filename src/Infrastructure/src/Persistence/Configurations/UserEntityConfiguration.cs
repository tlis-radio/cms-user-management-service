using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.ValueGeneration;
using Tlis.Cms.UserManagement.Domain.Models;

namespace Tlis.Cms.UserManagement.Infrastructure.Persistence.Configurations;

internal sealed class UserEntityConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).ValueGeneratedOnAdd().HasValueGenerator((_, _) => new GuidValueGenerator());

        builder
            .HasIndex(x => new { x.Firstname, x.Lastname, x.Nickname })
            .IsUnique();

        builder
            .Property(x => x.Id)
            .IsRequired()
            .ValueGeneratedOnAdd();

        builder
            .Property(x => x.Firstname)
            .IsRequired();
        builder
            .Property(x => x.Lastname)
            .IsRequired();
        builder
            .Property(x => x.Nickname)
            .IsRequired();
        builder
            .Property(x => x.Description)
            .IsRequired();
        builder
            .Property(x => x.ProfileImageUrl);
        builder
            .Property(x => x.IsActive)
            .IsRequired();
        builder
            .Property(x => x.MemberSinceDate)
            .IsRequired();
        builder
            .Property(x => x.MembershipEndedDate);
        builder
            .Property(x => x.MembershipEndedReason);
        builder
            .Property(x => x.ExternalId);
        builder
            .Property(x => x.Email);

        builder
            .HasMany(x => x.RoleHistory)
            .WithOne(x => x.User)
            .HasForeignKey(x => x.UserForeignKey)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
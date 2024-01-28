using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.ValueGeneration;
using Tlis.Cms.UserManagement.Domain.Entities;

namespace Tlis.Cms.UserManagement.Infrastructure.Persistence.Configurations;

internal sealed class UserMembershipHistoryEntityConfiguration : IEntityTypeConfiguration<UserMembershipHistory>
{
    public void Configure(EntityTypeBuilder<UserMembershipHistory> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).ValueGeneratedOnAdd().HasValueGenerator((_, _) => new GuidValueGenerator());

        builder.Property(x => x.UserId).IsRequired();
        builder.Property(x => x.ChangeDate).IsRequired();
        builder.Property(x => x.Status).HasConversion<string>().IsRequired();
        builder.Property(x => x.Description);

        builder.HasIndex(x => x.Id);
        builder.HasIndex(x => x.UserId);

        builder
            .HasOne<User>()
            .WithMany(x => x.MembershipHistory)
            .HasForeignKey(x => x.UserId)
            .OnDelete(DeleteBehavior.NoAction); // TODO: test
    }
}
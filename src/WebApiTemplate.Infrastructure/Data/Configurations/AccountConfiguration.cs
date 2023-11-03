using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebApiTemplate.Domain.Entities;
using WebApiTemplate.SharedKernel.Enums;

namespace WebApiTemplate.Infrastructure.Data.Configurations
{
    public class AccountConfiguration : AuditEntityConfiguration<Account>
    {
        public override void Configure(EntityTypeBuilder<Account> builder)
        {
            base.Configure(builder);

            builder.Property(b => b.AccountType)
                .IsRequired()
                .HasDefaultValue(AccountType.User);

            builder.Property(b => b.Status)
                .IsRequired()
                .HasDefaultValue(AccountStatus.Active);

            builder.HasMany(b => b.Roles)
                .WithOne(b => b.Account)
                .HasForeignKey(b => b.AccountId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(b => b.UserProfile)
                .WithOne(b => b.Account)
                .HasForeignKey<UserProfile>(b => b.AccountId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(b => b.ClientProfile)
                .WithOne(b => b.Account)
                .HasForeignKey<ClientProfile>(b => b.AccountId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(b => b.ExternalUserProfile)
                .WithOne(b => b.Account)
                .HasForeignKey<ExternalUserProfile>(b => b.AccountId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}

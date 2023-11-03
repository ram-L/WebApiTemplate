using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebApiTemplate.Domain.Entities;

namespace WebApiTemplate.Infrastructure.Data.Configurations
{
    public class AccountRoleConfiguration : EntityConfiguration<AccountRole>
    {
        public override void Configure(EntityTypeBuilder<AccountRole> builder)
        {
            base.Configure(builder);

            builder.Property(b => b.AccountId)
                .IsRequired();

            builder.Property(b => b.RoleId)
                .IsRequired();

            builder.HasIndex(b => new { b.AccountId, b.RoleId }).IsUnique();
        }
    }
}

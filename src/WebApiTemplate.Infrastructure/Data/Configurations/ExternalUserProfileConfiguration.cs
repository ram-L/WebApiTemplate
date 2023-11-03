using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebApiTemplate.Domain.Entities;

namespace WebApiTemplate.Infrastructure.Data.Configurations
{
    public class ExternalUserProfileConfiguration : EntityConfiguration<ExternalUserProfile>
    {
        public override void Configure(EntityTypeBuilder<ExternalUserProfile> builder)
        {
            base.Configure(builder);

            builder.Property(b => b.AccountId)
                .IsRequired();

            builder.Property(b => b.AuthProvider)
                .IsRequired();

            builder.Property(b => b.Email)
                .IsRequired()
                .HasMaxLength(320);

            builder.Property(b => b.ProviderUserId)
                .IsRequired(false)
                .HasMaxLength(150);

            builder.Property(b => b.TenantId)
               .IsRequired(false)
               .HasMaxLength(150);
        }
    }
}

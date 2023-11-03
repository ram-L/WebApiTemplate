using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebApiTemplate.Domain.Entities;

namespace WebApiTemplate.Infrastructure.Data.Configurations
{
    public class ClientProfileConfiguration : EntityConfiguration<ClientProfile>
    {
        public override void Configure(EntityTypeBuilder<ClientProfile> builder)
        {
            base.Configure(builder);

            builder.Property(b => b.AccountId)
                .IsRequired();

            builder.Property(b => b.ClientType)
                .IsRequired();

            builder.Property(b => b.ClientName)
                .IsRequired()
                .HasMaxLength(250);

            builder.Property(b => b.Description)
                .IsRequired(false);

            builder.Property(b => b.ClientKey)
                .IsRequired()
                .HasMaxLength(150);
        }
    }
}

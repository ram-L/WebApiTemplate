using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebApiTemplate.Domain.Entities;
using WebApiTemplate.SharedKernel.Enums;

namespace WebApiTemplate.Infrastructure.Data.Configurations
{
    public class RoleClaimConfiguration : EntityConfiguration<RoleClaim>
    {
        public override void Configure(EntityTypeBuilder<RoleClaim> builder)
        {
            base.Configure(builder);

            builder.Property(b => b.RoleId)
                .IsRequired();

            builder.Property(b => b.Resource)
                .IsRequired();

            builder.Property(b => b.Permission)
                .IsRequired()
                .HasDefaultValue(PermissionType.None);

            builder.HasIndex(b => new { b.RoleId, b.Resource }).IsUnique();
        }
    }
}

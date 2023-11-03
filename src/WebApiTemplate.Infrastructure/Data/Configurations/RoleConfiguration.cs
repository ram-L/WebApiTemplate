using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebApiTemplate.Domain.Entities;

namespace WebApiTemplate.Infrastructure.Data.Configurations
{
    public class RoleConfiguration : AuditEntityConfiguration<Role>
    {
        public override void Configure(EntityTypeBuilder<Role> builder)
        {
            base.Configure(builder);

            builder.Property(b => b.Name)
                .HasMaxLength(250)
                .IsRequired();

            builder.HasIndex(b => b.Name)
                .IsUnique();

            builder.Property(b => b.Description)
                .HasMaxLength(250)
                .IsRequired(false);

            builder.HasMany(b => b.Claims)
                .WithOne(b => b.Role)
                .HasForeignKey(b => b.RoleId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebApiTemplate.Domain.Interfaces;

namespace WebApiTemplate.Infrastructure.Data.Configurations
{
    public abstract class AuditEntityConfiguration<T> : EntityConfiguration<T> where T : class, IAuditEntity
    {
        public override void Configure(EntityTypeBuilder<T> builder)
        {
            base.Configure(builder);

            // DateCreated
            builder.Property(b => b.CreatedDate)
                .IsRequired()
                .HasDefaultValueSql("GETDATE()");

            // CreatedById
            builder.Property(b => b.CreatedById)
                .IsRequired(false);

            // CreatedByUser
            builder.HasOne(b => b.CreatedBy)
                .WithMany()
                .HasForeignKey(b => b.CreatedById)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.NoAction);

            // ModifiedDate
            builder.Property(b => b.ModifiedDate)
                .IsRequired(false);

            // ModifiedById
            builder.Property(b => b.ModifiedById)
                .IsRequired(false);

            // ModifiedByUser
            builder.HasOne(b => b.ModifiedBy)
                .WithMany()
                .HasForeignKey(b => b.ModifiedById)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.NoAction);

            // OwnerId
            builder.Property(b => b.OwnerId)
                .IsRequired(false);

            // Owner
            builder.HasOne(b => b.Owner)
                .WithMany()
                .HasForeignKey(b => b.OwnerId)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.NoAction);

            // RowVersion
            builder.Property(b => b.RowVersion)
                .IsConcurrencyToken()
                .ValueGeneratedOnAddOrUpdate()
                .IsRequired();

            // IsDeleted
            builder.Property(b => b.IsDeleted)
                .IsRequired()
                .HasDefaultValue(false);
        }
    }
}

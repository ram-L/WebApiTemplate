using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebApiTemplate.Domain.Interfaces;

namespace WebApiTemplate.Infrastructure.Data.Configurations
{
    public abstract class EntityConfiguration<T> : IEntityTypeConfiguration<T> where T : class, IEntity
    {
        public virtual void Configure(EntityTypeBuilder<T> builder)
        {
            builder.HasKey(b => b.Id);

            builder.Property(b => b.Id)
                .IsRequired()
                .ValueGeneratedOnAdd(); // Auto-Increment
        }
    }
}

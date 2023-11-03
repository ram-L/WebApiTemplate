using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebApiTemplate.Domain.Entities;

namespace WebApiTemplate.Infrastructure.Data.Configurations
{
    public class UserProfileConfiguration : EntityConfiguration<UserProfile>
    {
        public override void Configure(EntityTypeBuilder<UserProfile> builder)
        {
            base.Configure(builder);

            builder.Property(b => b.AccountId)
                .IsRequired();

            builder.Property(b => b.Username)
                .IsRequired();

            builder.Property(b => b.PasswordHash)
                .IsRequired()
                .HasMaxLength(500);

            builder.Property(b => b.Firstname)
                .IsRequired(false)
                .HasMaxLength(150);

            builder.Property(b => b.Surname)
                .IsRequired(false)
                .HasMaxLength(150);

            builder.Property(b => b.ContactNo)
                .IsRequired(false)
                .HasMaxLength(50);

            builder.Property(b => b.Email)
                .IsRequired()
                .HasMaxLength(320);

            builder.Property(b => b.Website)
                .IsRequired(false)
                .HasMaxLength(100);
        }
    }
}

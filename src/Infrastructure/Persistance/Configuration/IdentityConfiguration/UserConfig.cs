using Domain.Aggregates.Identity;
using Domain.Entities.IdentityModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistance.Configuration.IdentityConfiguration
{
    class UserConfig : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.Property(p => p.Id).HasDefaultValueSql("NEWSEQUENTIALID()");
            builder.Property(p => p.UserName).IsRequired().HasMaxLength(100);
            builder.Property(p => p.FullName).IsRequired().HasMaxLength(100);
            builder.Property(p => p.PasswordHash).IsRequired().HasMaxLength(500);
            builder.Property(p => p.RefreshToken).HasMaxLength(100);
            builder.ToTable("Users");
        }
    }

}

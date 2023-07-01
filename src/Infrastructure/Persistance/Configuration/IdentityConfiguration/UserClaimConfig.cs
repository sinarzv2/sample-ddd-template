using Domain.Entities.IdentityModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistance.Configuration.IdentityConfiguration
{
    public class UserClaimConfig : IEntityTypeConfiguration<UserClaim>
    {
        public void Configure(EntityTypeBuilder<UserClaim> builder)
        {
            builder.Property(p => p.Id).HasDefaultValueSql("NEWSEQUENTIALID()");
            builder.HasIndex(p => new { p.ClaimType, p.ClaimValue }).IsUnique();
            builder.ToTable("UserClaims");
        }
    }
}

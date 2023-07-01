using Domain.Entities.IdentityModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistance.Configuration.IdentityConfiguration
{
   
        public class UserTokenConfig : IEntityTypeConfiguration<UserToken>
        {
            public void Configure(EntityTypeBuilder<UserToken> builder)
            {
                builder.ToTable("UserTokens");
            }
        }
    
}

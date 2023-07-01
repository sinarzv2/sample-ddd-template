using Domain.Entities.IdentityModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistance.Configuration.IdentityConfiguration
{

    public class UserLoginConfig : IEntityTypeConfiguration<UserLogin>
    {
        public void Configure(EntityTypeBuilder<UserLogin> builder)
        {

            builder.ToTable("UserLogins");
        }
    }

}

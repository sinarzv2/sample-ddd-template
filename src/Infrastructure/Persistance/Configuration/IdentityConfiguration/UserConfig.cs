using Domain.Aggregates.Identity;
using Domain.Aggregates.Identity.ValueObjects;
using Domain.SharedKernel.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistance.Configuration.IdentityConfiguration
{
    class UserConfig : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.Property(p => p.Id).HasDefaultValueSql("NEWSEQUENTIALID()");
            builder.Property(p => p.UserName).IsRequired().HasMaxLength(Username.MaxLength);
            builder.Property(p => p.PasswordHash).IsRequired().HasMaxLength(500);
            builder.Property(p => p.RefreshToken).HasMaxLength(100);
            builder.ToTable("Users");

            builder
                .Property(u => u.BirthDate)
                .HasColumnName(nameof(User.BirthDate))
                .IsRequired()
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .HasConversion(birthDate => birthDate.Value, value => BirthDate.Create(value).Data);


            builder.OwnsOne(p => p.FullName, p =>
            {
                p.HasOne(pp => pp.Gender)
                    .WithMany()
                    .HasForeignKey("GenderId")
                    .IsRequired();

                p.Property<int>("GenderId")
                    .HasColumnName("GenderId")
                    .IsRequired()
                    .UsePropertyAccessMode(PropertyAccessMode.Field);

                p.Property(d => d.FirstName)
                    .HasColumnName(nameof(FullName.FirstName))
                    .IsRequired()
                    .HasMaxLength(FirstName.MaxLength)
                    .UsePropertyAccessMode(PropertyAccessMode.Field)
                    .HasConversion(lastName => lastName.Value, value => FirstName.Create(value).Data);

                p.Property(pp => pp.LastName)
                    .IsRequired()
                    .UsePropertyAccessMode(PropertyAccessMode.Field)
                    .HasColumnName(nameof(FullName.LastName))
                    .HasMaxLength(LastName.MaxLength)
                    .HasConversion(lastName => lastName.Value, value => LastName.Create(value).Data);
            });

        }
    }

}

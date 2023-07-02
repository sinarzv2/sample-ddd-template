﻿using Domain.Aggregates.Identity;
using Domain.Entities.IdentityModel;
using Domain.SharedKernel.ValueObjects;
using Microsoft.AspNetCore.Identity;
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
            builder.Property(p => p.PasswordHash).IsRequired().HasMaxLength(500);
            builder.Property(p => p.RefreshToken).HasMaxLength(100);
            builder.ToTable("Users");


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


                p.Property(pp => pp.FirstName)
                    .IsRequired()
                    .UsePropertyAccessMode(PropertyAccessMode.Field)
                    .HasColumnName(nameof(FullName.FirstName))
                    .HasMaxLength(FirstName.MaxLength)
                    .HasConversion(firstName => firstName.Value, value => FirstName.Create(value).Data);

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
using Domain.SharedKernel.Enumerations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistance.Configuration.SharedKernel;

class GenderConfig : IEntityTypeConfiguration<Gender>
{
    public void Configure(EntityTypeBuilder<Gender> builder)
    {
        builder.ToTable("Genders");

        builder.HasKey(p => p.Value);

        builder.Property(p => p.Value)
            .ValueGeneratedNever()
            .IsRequired();

        builder
            .Property(p => p.Name)
            .IsRequired()
            .HasMaxLength(Gender.MaxLength);


        builder.HasData(Gender.Male, Gender.Female);

    }
}
using Domain.Aggregates.Cities;
using Domain.SharedKernel.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistance.Configuration.CityConfiguration;

class Cityconfig : IEntityTypeConfiguration<City>
{
    public void Configure(EntityTypeBuilder<City> builder)
    {
        builder.Property(p => p.Id).HasDefaultValueSql("NEWSEQUENTIALID()");

        builder
            .Property(p => p.Name)
            .IsRequired()
            .HasMaxLength(Name.MaxLength)
            .HasConversion(p => p.Value, p => Name.Create(p).Data);
    }
}
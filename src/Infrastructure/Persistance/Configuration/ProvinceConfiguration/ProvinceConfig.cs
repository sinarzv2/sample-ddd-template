using Domain.Aggregates.Provinces;
using Domain.SharedKernel.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistance.Configuration.ProvinceConfiguration
{
    class ProvinceConfigc : IEntityTypeConfiguration<Province>
    {
        public void Configure(EntityTypeBuilder<Province> builder)
        {
            builder.Property(p => p.Id).HasDefaultValueSql("NEWSEQUENTIALID()");

            builder
                .Property(p => p.Name)
                .IsRequired()
                .HasMaxLength(Name.MaxLength)
                .HasConversion(p => p.Value, p => Name.Create(p).Data);

            builder
                .HasIndex(p => p.Name)
                .IsUnique();
           
            builder
                .HasMany(current => current.Cities)
                .WithOne(current => current.Province)
                .IsRequired()
                .HasForeignKey(current => current.ProvinceId)
                .OnDelete(DeleteBehavior.NoAction);
            

            builder.HasData(
                Province.Create("Tehran", Guid.Parse("AD6FBB80-48CE-4788-B940-50F6D522B70B")).Data,
                Province.Create("Alborz", Guid.Parse("9500A965-1D6B-4F49-9993-6E8B6C405CA8")).Data);

        }
    }
}

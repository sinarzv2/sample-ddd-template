using Domain.Aggregates.DiscountCoupons;
using Domain.Aggregates.DiscountCoupons.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistance.Configuration.DiscountCouponConfiguration;

class DiscountCouponConfig : IEntityTypeConfiguration<DiscountCoupon>
{
    public void Configure(EntityTypeBuilder<DiscountCoupon> builder)
    {
        builder.Property(p => p.Id).HasDefaultValueSql("NEWSEQUENTIALID()");

        builder
            .Property(p => p.DiscountPercent)
            .IsRequired()
            .HasConversion(p => p.Value, p => DiscountPercent.Create(p).Data);

        builder
            .Property(p => p.ValidDateFrom)
            .IsRequired()
            .HasConversion(p => p.Value, p => ValidDateFrom.Create(p).Data);

        builder
            .Property(p => p.ValidDateTo)
            .IsRequired()
            .HasConversion(p => p.Value, p => ValidDateTo.Create(p).Data);
    }
}
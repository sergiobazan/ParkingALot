using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ParkingALot.Domain.Bookings;
using ParkingALot.Domain.Drivers;
using ParkingALot.Domain.ParkingLotOwners;
using ParkingALot.Domain.Shared;

namespace ParkingALot.Infrastructure.Configurations;

internal sealed class BookingConfiguration : IEntityTypeConfiguration<Booking>
{
    public void Configure(EntityTypeBuilder<Booking> builder)
    {
        builder.ToTable("bookings");

        builder.HasKey(booking => booking.Id);

        builder.OwnsOne(booking => booking.PriceForPeriod, priceBuilder =>
        {
            priceBuilder.Property(price => price.Currency)
                .HasConversion(currency => currency.Code, code => Currency.FromCode(code));
        });

        builder.OwnsOne(booking => booking.ServicesPrice, priceBuilder =>
        {
            priceBuilder.Property(price => price.Currency)
                .HasConversion(currency => currency.Code, code => Currency.FromCode(code));
        });

        builder.OwnsOne(booking => booking.PointsDiscount, priceBuilder =>
        {
            priceBuilder.Property(price => price.Currency)
                .HasConversion(currency => currency.Code, code => Currency.FromCode(code));
        });

        builder.OwnsOne(booking => booking.TotalPrice, priceBuilder =>
        {
            priceBuilder.Property(price => price.Currency)
                .HasConversion(currency => currency.Code, code => Currency.FromCode(code));
        });

        builder.OwnsOne(booking => booking.Range);

        builder
            .HasOne<Driver>()
            .WithMany()
            .HasForeignKey(booking => booking.DriverId);

        builder
            .HasOne<ParkingLot>()
            .WithMany()
            .HasForeignKey(booking => booking.ParkingLotId);
    }
}

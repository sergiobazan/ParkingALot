using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ParkingALot.Domain.Bookings;
using ParkingALot.Domain.ParkingLotOwners;
using ParkingALot.Domain.Shared;

namespace ParkingALot.Infrastructure.Configurations;

internal sealed class ParkingLotConfiguration : IEntityTypeConfiguration<ParkingLot>
{
    public void Configure(EntityTypeBuilder<ParkingLot> builder)
    {
        builder.ToTable("parking_lots");

        builder.HasKey(p => p.Id);

        builder.Property(p => p.Name)
            .HasConversion(name => name.Value, value => new Name(value));

        builder.Property(p => p.Description)
            .HasConversion(description => description.Value, value => new Description(value));

        builder.OwnsOne(p => p.Address);

        builder.OwnsOne(p => p.PricePerHour, priceBuilder =>
        {
            priceBuilder
                .Property(money => money.Currency)
                .HasConversion(currency => currency.Code, code => Currency.FromCode(code));
        });

        builder.HasMany(parking => parking.Services)
            .WithOne()
            .HasForeignKey(service => service.ParkingLotId);

        builder.HasMany<Booking>()
            .WithOne()
            .HasForeignKey(booking => booking.ParkingLotId);
    }
}

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ParkingALot.Domain.Bookings;
using ParkingALot.Domain.ParkingLotOwners;
using ParkingALot.Domain.Shared;

namespace ParkingALot.Infrastructure.Configurations;

internal sealed class BookingItemConfiguration : IEntityTypeConfiguration<BookingItem>
{
    public void Configure(EntityTypeBuilder<BookingItem> builder)
    {
        builder.ToTable("booking_items");

        builder.HasKey(b => b.Id);

        builder.OwnsOne(b => b.Price, priceBuilder =>
        {
            priceBuilder.Property(money => money.Currency)
                .HasConversion(currency => currency.Code, code => Currency.FromCode(code));
        });

        builder
            .HasOne<Booking>()
            .WithMany()
            .HasForeignKey(b => b.BookingId);

        builder
            .HasOne<Service>()
            .WithOne()
            .HasForeignKey<BookingItem>(b => b.ServiceId);
    }
}

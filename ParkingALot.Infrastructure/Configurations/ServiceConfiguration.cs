using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ParkingALot.Domain.ParkingLotOwners;
using ParkingALot.Domain.Shared;

namespace ParkingALot.Infrastructure.Configurations;

internal sealed class ServiceConfiguration : IEntityTypeConfiguration<Service>
{
    public void Configure(EntityTypeBuilder<Service> builder)
    {
        builder.ToTable("services");

        builder.HasKey(service => service.Id);

        builder.Property(service => service.Name)
           .HasConversion(name => name.Value, value => new Name(value));

        builder.Property(service => service.Description)
            .HasConversion(description => description.Value, value => new Description(value));

        builder.OwnsOne(service => service.Price, priceBuilder =>
        {
            priceBuilder.Property(money => money.Currency)
                .HasConversion(currency => currency.Code, code => Currency.FromCode(code));
        });

        builder.Property(service => service.ImageUrl)
            .HasConversion(imageUrl => imageUrl.Value, value => new Image(value));
    }
}

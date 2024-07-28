using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ParkingALot.Domain.Drivers;

namespace ParkingALot.Infrastructure.Configurations;

internal sealed class VehicleConfiguration : IEntityTypeConfiguration<Vehicle>
{
    public void Configure(EntityTypeBuilder<Vehicle> builder)
    {
        builder.ToTable("vehicles");

        builder.HasKey(vehicle => vehicle.Id);

        builder.OwnsOne(vehicle => vehicle.Characteristics);

    }
}

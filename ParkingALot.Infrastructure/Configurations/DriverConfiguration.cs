using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ParkingALot.Domain.Drivers;
using ParkingALot.Domain.Shared;

namespace ParkingALot.Infrastructure.Configurations;

internal sealed class DriverConfiguration : IEntityTypeConfiguration<Driver>
{
    public void Configure(EntityTypeBuilder<Driver> builder)
    {
        builder.ToTable("drivers");

        builder.HasKey(driver => driver.Id);

        builder.Property(driver => driver.Name)
            .HasConversion(name => name.Value, value => new Name(value))
            .HasMaxLength(50);

        builder.Property(driver => driver.Email)
            .HasConversion(email => email.Value, value => new Email(value))
            .HasMaxLength(50);

        builder.Property(driver => driver.TotalPoints)
            .HasConversion(point => point.Total, total => Point.Create(total).Value);

        builder
            .HasMany(driver => driver.Vehicles)
            .WithOne()
            .HasForeignKey(vehicle => vehicle.DriverId);

        builder.HasIndex(driver => driver.Email).IsUnique();
    }
}

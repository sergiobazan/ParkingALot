using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ParkingALot.Domain.ParkingLotOwners;
using ParkingALot.Domain.Shared;

namespace ParkingALot.Infrastructure.Configurations;

internal sealed class ParkingLotOwnerConfiguration : IEntityTypeConfiguration<ParkingLotOwner>
{
    public void Configure(EntityTypeBuilder<ParkingLotOwner> builder)
    {
        builder.ToTable("parking_lot_owners");

        builder.HasKey(o => o.Id);

        builder.Property(owner => owner.Name)
           .HasConversion(name => name.Value, value => new Name(value))
           .HasMaxLength(50);

        builder.Property(owner => owner.Email)
            .HasConversion(email => email.Value, value => new Email(value))
            .HasMaxLength(50);

        builder.HasIndex(owner => owner.Email).IsUnique();
    }
}

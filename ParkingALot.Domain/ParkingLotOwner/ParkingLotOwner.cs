﻿using ParkingALot.Domain.Abstractions;
using ParkingALot.Domain.Shared;

namespace ParkingALot.Domain.ParkingLotOwner;

public sealed class ParkingLotOwner : Entity
{
    private readonly List<ParkingLot> _parkingLots = new();
    private ParkingLotOwner(Guid id, Name name, Email email) : base(id)
    {
        Name = name;
        Email = email;
    }

    public Name Name { get; private set; }
    public Email Email { get; private set; }
    public IReadOnlyList<ParkingLot> ParkingLots => _parkingLots.ToList();

    public static ParkingLotOwner Create(Name name, Email email)
    {
        var owner = new ParkingLotOwner(Guid.NewGuid(), name, email);

        return owner;
    }

    public void AddParkingLot(
        Name name,
        Description description,
        Address address,
        DateTime openAtUtc,
        DateTime closeAtUtc)
    {
        var parkingLot = new ParkingLot(Guid.NewGuid(), Id, name, description, address, openAtUtc, closeAtUtc);

        _parkingLots.Add(parkingLot);
    }
}

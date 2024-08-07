﻿using ParkingALot.Domain.Abstractions;
using ParkingALot.Domain.Bookings.DomainEvents;
using ParkingALot.Domain.Drivers;
using ParkingALot.Domain.ParkingLotOwners;
using ParkingALot.Domain.Shared;

namespace ParkingALot.Domain.Bookings;

public sealed class Booking : Entity
{
    private readonly List<BookingItem> _bookingItems = new();
    private Booking() { }
    private Booking(
        Guid id,
        Guid driverId,
        Guid parkingLotId,
        Money priceForPeriod,
        Money servicesPrice,
        Money pointsDiscount,
        Money totalPrice,
        DateRange range,
        BookingStatus status,
        DateTime createdOneUtc) : base(id)
    {
        DriverId = driverId;
        ParkingLotId = parkingLotId;
        PriceForPeriod = priceForPeriod;
        ServicesPrice = servicesPrice;
        PointsDiscount = pointsDiscount;
        TotalPrice = totalPrice;
        Range = range;
        Status = status;
        CreatedOneUtc = createdOneUtc;
    }

    public Guid DriverId { get; private set; }
    public Guid ParkingLotId { get; private set; }
    public Money PriceForPeriod { get; private set; }
    public Money ServicesPrice {  get; private set; }
    public Money PointsDiscount { get; private set; }
    public Money TotalPrice { get; private set; }
    public DateRange Range { get; private set; }
    public BookingStatus Status { get; private set; }
    public DateTime CreatedOneUtc { get; private set; }
    public DateTime? CompletedOnUtc { get; private set; }
    public DateTime? CancelledOnUtc { get; private set; }
    public IReadOnlyList<BookingItem> BookingItems => _bookingItems.ToList();

    public static Result<Booking> Reserve(
        Driver driver,
        ParkingLot parkingLot,
        PriceService priceService,
        DateRange timeRange,
        DateTime createdOneUtc)
    {
        var pricingDetails = priceService.GetTotalPrice(parkingLot, timeRange, Enumerable.Empty<BookingItem>());

        var booking = new Booking(
            Guid.NewGuid(),
            driver.Id,
            parkingLot.Id,
            pricingDetails.PriceForPeriod,
            pricingDetails.ServicesPrice,
            pricingDetails.PointsDiscount,
            pricingDetails.TotalPrice,
            timeRange,
            BookingStatus.Reserved,
            createdOneUtc);

        driver.AddPoints(timeRange.LengthInHours);

        booking.RaiseDomainEvent(new BookingReservedDomainEvent(booking.Id));

        return booking;    
    }

    public Result<BookingItem> AddBookingItem(Service service)
    {
        var bookingItem = new BookingItem(Guid.NewGuid(), Id, service.Id, service.Price);

        _bookingItems.Add(bookingItem);

        RaiseDomainEvent(new ServiceAddedToBookingDomainEvent(Id, ParkingLotId));

        return bookingItem;
    }

    public void RecalculatePrices(
        ParkingLot parkingLot,
        PriceService priceService,
        bool usePoints = false,
        int points = 0)
    {
        var pricingDetails = priceService.GetTotalPrice(parkingLot, Range, _bookingItems, usePoints, points);

        PriceForPeriod = pricingDetails.PriceForPeriod;
        ServicesPrice = pricingDetails.ServicesPrice;
        PointsDiscount = pricingDetails.PointsDiscount;
        TotalPrice = pricingDetails.TotalPrice;
    }
}
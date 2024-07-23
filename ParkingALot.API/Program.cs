using ParkingALot.API.Endpoints.Bookings;
using ParkingALot.API.Endpoints.Drivers;
using ParkingALot.API.Endpoints.ParkingLotOwners;
using ParkingALot.API.Extensions;
using ParkingALot.API.Middlewares;
using ParkingALot.Application;
using ParkingALot.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services
    .AddApplication()
    .AddInfrastructure(builder.Configuration);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

    app.ApplyMigration();
}

app.UseHttpsRedirection();

app.UseMiddleware<ValidationMiddleware>();

app.AddDriversEndpoints();

app.AddParkingLotOwnerEndpoints();

app.AddBookinEndpoints();

app.Run();

using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using ParkingALot.Application.Abstractions.Behaviors;
using ParkingALot.Domain.Bookings;

namespace ParkingALot.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        var assembly = typeof(DependencyInjection).Assembly;

        services.AddMediatR(configuration =>
        {
            configuration.RegisterServicesFromAssembly(assembly);

            configuration.AddOpenBehavior(typeof(ValidationBehavior<,>));
        });

        services.AddValidatorsFromAssembly(assembly);

        services.AddTransient<PriceService>();

        return services;
    }
}

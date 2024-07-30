using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ParkingALot.Application.Abstractions.Clock;
using ParkingALot.Application.Abstractions.DbContext;
using ParkingALot.Application.Abstractions.Email;
using ParkingALot.Domain.Abstractions;
using ParkingALot.Domain.Bookings;
using ParkingALot.Domain.Drivers;
using ParkingALot.Domain.ParkingLotOwners;
using ParkingALot.Infrastructure.BackgroundJob;
using ParkingALot.Infrastructure.Clock;
using ParkingALot.Infrastructure.Email;
using ParkingALot.Infrastructure.Outbox;
using ParkingALot.Infrastructure.Repositories;
using Quartz;

namespace ParkingALot.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddTransient<IEmailService, EmailService>();
        services.AddTransient<IDateTimeProvider, DateTimeProvider>();

        var connectionString = configuration.GetConnectionString("Database") ??
            throw new ArgumentNullException(nameof(configuration));

        services.AddSingleton<OutboxMessageInterceptor>();

        services.AddDbContext<ApplicationDbContext>((sp, options) =>
            options.UseNpgsql(connectionString)
                .AddInterceptors(sp.GetService<OutboxMessageInterceptor>()!)
                .UseSnakeCaseNamingConvention());

        services.AddScoped<IUnitOfWork>(sp =>
            sp.GetRequiredService<ApplicationDbContext>());

        services.AddScoped<IApplicationDbContext>(sp =>
            sp.GetRequiredService<ApplicationDbContext>());

        services.AddScoped<IDriversRepository, DriverRepository>();
        services.AddScoped<IVehicleRepository, VehicleRepository>();
        services.AddScoped<IParkingLotOwnerRepository, ParkingLotOwnerRepository>();
        services.AddScoped<IParkingLotRepository, ParkingLotRepository>();
        services.AddScoped<IServiceRepository, ServiceRepository>();
        services.AddScoped<IBookingRepository, BookingRepository>();
        services.AddScoped<IBookingItemRepository, BookingItemRepository>();

        services.AddQuartz(configure =>
        {
            var jobKey = new JobKey(nameof(ProcessDomainEventsJob));

            configure.AddJob<ProcessDomainEventsJob>(opt => opt.WithIdentity(jobKey));

            configure.AddTrigger(trigger => 
                trigger
                    .ForJob(jobKey)
                    .WithSimpleSchedule(schedule => 
                        schedule
                            .WithIntervalInSeconds(10)
                            .RepeatForever()));
        });

        services.AddQuartzHostedService(options => options.WaitForJobsToComplete = true);

        return services;
    }
}

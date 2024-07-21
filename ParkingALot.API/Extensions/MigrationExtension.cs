using Microsoft.EntityFrameworkCore;
using ParkingALot.Infrastructure;

namespace ParkingALot.API.Extensions;

public static class MigrationExtension
{
    public static void ApplyMigration(this IApplicationBuilder app)
    {
        using var scope = app.ApplicationServices.CreateScope();

        using var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

        dbContext.Database.Migrate();
    }
}

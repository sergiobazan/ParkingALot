using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Newtonsoft.Json;
using ParkingALot.Domain.Abstractions;

namespace ParkingALot.Infrastructure.Outbox;
public class OutboxMessageInterceptor : SaveChangesInterceptor
{
    private static readonly JsonSerializerSettings SerializerSettings = new()
    {
        TypeNameHandling = TypeNameHandling.All,
    };

    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(
            DbContextEventData eventData,
            InterceptionResult<int> result,
            CancellationToken cancellationToken = default)
    {
        var context = eventData.Context;

        if (context is not null)
        {
            ProcessDomainEvents(context, cancellationToken);
        }

        return base.SavingChangesAsync(eventData, result, cancellationToken);
    }

    private static void ProcessDomainEvents(DbContext context, CancellationToken cancellationToken = default)
    {
        DateTime utcNow = DateTime.UtcNow;

        var outboxMessages = context
            .ChangeTracker
            .Entries<Entity>()
            .Select(entity => entity.Entity)
            .SelectMany(entity =>
            {
                var domainEvents = entity.DomainEvents;

                entity.ClearDomainEvents();

                return domainEvents;
            })
            .Select(domainEvent => new OutboxMessage
            {
                Id = Guid.NewGuid(),
                Type = domainEvent.GetType().Name,
                Content = JsonConvert.SerializeObject(domainEvent, SerializerSettings),
                OcurredAtUtc = utcNow,
            })
            .ToList();

        context.Set<OutboxMessage>().AddRange(outboxMessages);
    }
}

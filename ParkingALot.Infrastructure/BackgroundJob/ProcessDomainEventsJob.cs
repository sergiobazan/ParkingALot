using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using ParkingALot.Infrastructure.Outbox;
using Quartz;

namespace ParkingALot.Infrastructure.BackgroundJob;

[DisallowConcurrentExecution]
internal class ProcessDomainEventsJob(
    ApplicationDbContext dbContext,
    IPublisher publisher,
    ILogger<ProcessDomainEventsJob> logger) : IJob
{
    private static readonly JsonSerializerSettings SerializerSettings = new()
    {
        TypeNameHandling = TypeNameHandling.All,
    };

    public async Task Execute(IJobExecutionContext context)
    {
        logger.LogInformation("Start processing domain events");

        List<OutboxMessage> outboxMessages = await GetOutboxMessages(dbContext);

        if (outboxMessages.Count == 0)
        {
            logger.LogInformation("Domain Events already proccess");
            return;
        }

        foreach (OutboxMessage? outboxMessage in outboxMessages)
        {
            try
            {
                var domainEvent = JsonConvert.DeserializeObject(outboxMessage.Content, SerializerSettings)!;

                await publisher.Publish(domainEvent);

                logger.LogInformation("Domain Events Publish Successfully");
            }
            catch (Exception ex)
            {
                logger.LogError("Error Ocurred while processing domain event");

                outboxMessage.Error = ex.Message;
            }

            outboxMessage.ProcessAtUtc = DateTime.UtcNow;
        }

        await dbContext.SaveChangesAsync();
    }

    private static async Task<List<OutboxMessage>> GetOutboxMessages(ApplicationDbContext dbContext)
    {
        return await dbContext
            .Set<OutboxMessage>()
            .Where(message => message.ProcessAtUtc == null)
            .OrderBy(message => message.OcurredAtUtc)
            .Take(20)
            .ToListAsync();
    }
}

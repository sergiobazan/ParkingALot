namespace ParkingALot.Infrastructure.Outbox;
internal class OutboxMessage
{
    public Guid Id { get; set; }
    public string Type { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
    public DateTime OcurredAtUtc { get; set; }
    public string? Error { get; set; }
    public DateTime? ProcessAtUtc { get; set; }
}

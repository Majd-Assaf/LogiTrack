namespace Logistics.Contracts.Common;

public abstract class BaseMessage
{
    public Guid MessageId { get; init; } = Guid.NewGuid();
    public DateTime OccurredAtUtc { get; init; } = DateTime.UtcNow;
}

namespace Logistics.Contracts.Common;

public sealed class MessageMetadata
{
    public Guid CorrelationId { get; init; }
    public string Source { get; init; } = default!;
    public string? UserId { get; init; }
    public string? TenantId { get; init; }
}

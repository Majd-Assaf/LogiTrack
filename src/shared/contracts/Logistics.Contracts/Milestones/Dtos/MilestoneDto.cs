namespace Logistics.Contracts.Milestones.Dtos;

public sealed class MilestoneDto
{
    public Guid MilestoneId { get; init; }
    public Guid ShipmentId { get; init; }
    public string Type { get; init; } = default!;
    public string Location { get; init; } = default!;
    public DateTime OccurredAt { get; init; }
    public string Source { get; init; } = default!;
}

using Logistics.Contracts.Common;

namespace Logistics.Contracts.Milestones.Commands;

public sealed class AddMilestoneCommand : BaseMessage
{
    public required Guid ShipmentId { get; init; }
    public required string Type { get; init; }
    public required string Location { get; init; }
    public required DateTime OccurredAt { get; init; }
    public required string Source { get; init; }
}

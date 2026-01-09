using Logistics.Contracts.Common;
using Logistics.Contracts.Milestones.Dtos;

namespace Logistics.Contracts.Milestones.Events;

public sealed class MilestoneAddedEvent : BaseMessage
{
    public required MilestoneDto Milestone { get; init; }
}

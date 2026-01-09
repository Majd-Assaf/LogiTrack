using Logistics.Contracts.Common;
using Logistics.Contracts.Shipments.Dtos;

namespace Logistics.Contracts.Shipments.Events;

public sealed class ShipmentUpdatedEvent : BaseMessage
{
    public required ShipmentDto Shipment { get; init; }
}

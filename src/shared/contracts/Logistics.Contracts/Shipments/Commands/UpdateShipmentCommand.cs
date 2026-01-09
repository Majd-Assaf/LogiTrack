using Logistics.Contracts.Common;

namespace Logistics.Contracts.Shipments.Commands;

public sealed class UpdateShipmentCommand : BaseMessage
{
    public required Guid ShipmentId { get; init; }

    public string? Carrier { get; init; }
    public DateTime? ETD { get; init; }
    public DateTime? ETA { get; init; }
    public string? VesselOrFlight { get; init; }
}

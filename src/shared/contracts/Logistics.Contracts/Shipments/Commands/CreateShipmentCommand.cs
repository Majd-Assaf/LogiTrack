using Logistics.Contracts.Common;

namespace Logistics.Contracts.Shipments.Commands;

public sealed class CreateShipmentCommand : BaseMessage
{
    public required string ShipmentReference { get; init; }
    public required string Mode { get; init; }            // Air / Ocean
    public required string Origin { get; init; }
    public required string Destination { get; init; }

    public string? Carrier { get; init; }
    public DateTime? ETD { get; init; }
    public DateTime? ETA { get; init; }
}

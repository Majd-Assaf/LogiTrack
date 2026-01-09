namespace Logistics.Contracts.Shipments.Dtos;

public sealed class ShipmentDto
{
    public Guid ShipmentId { get; init; }
    public string ShipmentReference { get; init; } = default!;
    public string Mode { get; init; } = default!;
    public string Origin { get; init; } = default!;
    public string Destination { get; init; } = default!;
    public string? Carrier { get; init; }
    public string? VesselOrFlight { get; init; }
    public DateTime? ETD { get; init; }
    public DateTime? ETA { get; init; }
    public string Status { get; init; } = default!;
}

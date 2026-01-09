namespace Logistics.Contracts.Documents.Dtos;

public sealed class DocumentDto
{
    public Guid DocumentId { get; init; }
    public Guid ShipmentId { get; init; }
    public string Type { get; init; } = default!;
    public string FileName { get; init; } = default!;
    public string ContentType { get; init; } = default!;
    public string StorageUrl { get; init; } = default!;
    public string Checksum { get; init; } = default!;
    public DateTime UploadedAt { get; init; }
}

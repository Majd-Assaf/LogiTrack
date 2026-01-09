using Logistics.Contracts.Common;

namespace Logistics.Contracts.Documents.Commands;

public sealed class UploadDocumentCommand : BaseMessage
{
    public required Guid ShipmentId { get; init; }
    public required string Type { get; init; }
    public required string FileName { get; init; }
    public required string ContentType { get; init; }
    public required string StorageUrl { get; init; }
    public required string Checksum { get; init; }
}

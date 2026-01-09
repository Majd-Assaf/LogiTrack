using Logistics.Contracts.Common;
using Logistics.Contracts.Documents.Dtos;

namespace Logistics.Contracts.Documents.Events;

public sealed class DocumentUploadedEvent : BaseMessage
{
    public required DocumentDto Document { get; init; }
}

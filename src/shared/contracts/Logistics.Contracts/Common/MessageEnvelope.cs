using Logistics.Contracts.Versioning;

namespace Logistics.Contracts.Common;

public sealed class MessageEnvelope<T> where T : BaseMessage
{
    public required T Payload { get; init; }
    public required MessageMetadata Metadata { get; init; }
    public required ContractVersion Version { get; init; }
}

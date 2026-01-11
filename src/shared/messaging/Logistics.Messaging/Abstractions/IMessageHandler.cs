using Logistics.Contracts.Common;

namespace Logistics.Messaging.Abstractions;

public interface IMessageHandler<T> where T : BaseMessage
{
    Task HandleAsync(
        MessageEnvelope<T> envelope,
        CancellationToken cancellationToken);
}

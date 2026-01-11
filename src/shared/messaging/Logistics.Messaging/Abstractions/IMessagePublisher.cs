using Logistics.Contracts.Common;

namespace Logistics.Messaging.Abstractions;

public interface IMessagePublisher
{
    Task PublishCommandAsync<T>(
        MessageEnvelope<T> envelope,
        string routingKey,
        CancellationToken cancellationToken = default)
        where T : BaseMessage;

    Task PublishEventAsync<T>(
        MessageEnvelope<T> envelope,
        string routingKey,
        CancellationToken cancellationToken = default)
        where T : BaseMessage;
}

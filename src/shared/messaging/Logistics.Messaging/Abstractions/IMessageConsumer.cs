namespace Logistics.Messaging.Abstractions;

public interface IMessageConsumer
{
    Task StartAsync(CancellationToken cancellationToken);
}

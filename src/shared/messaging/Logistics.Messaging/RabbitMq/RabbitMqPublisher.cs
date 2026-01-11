using Logistics.Contracts.Common;
using Logistics.Messaging.Abstractions;
using Logistics.Messaging.Configuration;
using Logistics.Messaging.Serialization;
using RabbitMQ.Client;

namespace Logistics.Messaging.RabbitMq;

public sealed class RabbitMqPublisher : IMessagePublisher
{
    private readonly RabbitMqConnectionFactory _factory;
    private readonly RabbitMqOptions _options;

    public RabbitMqPublisher(
        RabbitMqConnectionFactory factory,
        RabbitMqOptions options)
    {
        _factory = factory;
        _options = options;
    }

    public Task PublishCommandAsync<T>(
        MessageEnvelope<T> envelope,
        string routingKey,
        CancellationToken cancellationToken = default)
        where T : BaseMessage
    {
        Publish(
            ExchangeNames.Commands(_options.Environment),
            routingKey,
            envelope);

        return Task.CompletedTask;
    }

    public Task PublishEventAsync<T>(
        MessageEnvelope<T> envelope,
        string routingKey,
        CancellationToken cancellationToken = default)
        where T : BaseMessage
    {
        Publish(
            ExchangeNames.Events(_options.Environment),
            routingKey,
            envelope);

        return Task.CompletedTask;
    }

    private void Publish<T>(
        string exchange,
        string routingKey,
        MessageEnvelope<T> envelope)
    {
        using var connection = _factory.CreateConnection();
        using var channel = connection.CreateModel();

        channel.ExchangeDeclare(exchange, ExchangeType.Topic, durable: true);

        var body = MessageSerializer.Serialize(envelope);

        channel.BasicPublish(
            exchange: exchange,
            routingKey: routingKey,
            mandatory: false,
            basicProperties: null,
            body: body);
    }
}

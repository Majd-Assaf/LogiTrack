using System.Text;
using System.Text.Json;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Polly;
using RabbitMQ.Client;
using RabbitMQ.Client.Exceptions;

namespace TransportService.Infrastructure.Messaging;

public interface IEventPublisher
{
    Task PublishAsync<T>(string topic, T @event);
}

public class RabbitMqEventPublisher : IEventPublisher
{
    private readonly IConfiguration _config;
    private readonly ConnectionFactory _factory;
    private readonly ILogger<RabbitMqEventPublisher> _logger;

    public RabbitMqEventPublisher(IConfiguration config, ILogger<RabbitMqEventPublisher> logger)
    {
        _config = config;
        _logger = logger;
        _factory = new ConnectionFactory
        {
            HostName = _config.GetSection("RabbitMQ:Host").ToString() ?? "rabbitmq",
            UserName = _config["RabbitMQ:User"] ?? "guest",
            Password = _config["RabbitMQ:Password"] ?? "guest"
        };
    }

    public Task PublishAsync<T>(string topic, T @event)
    {
        var retryPolicy = Policy
            .Handle<BrokerUnreachableException>()
            .WaitAndRetry(new[]
            {
                TimeSpan.FromSeconds(5),
                TimeSpan.FromSeconds(10),
                TimeSpan.FromSeconds(15)
            }, (exception, timeSpan, retryCount, context) =>
            {
                _logger.LogWarning(exception, "Error connecting to RabbitMQ. Retrying in {timeSpan}. Attempt {retryCount}", timeSpan, retryCount);
            });

        retryPolicy.Execute(() =>
        {
            using var conn = _factory.CreateConnection();
            using var channel = conn.CreateModel();
            channel.ExchangeDeclare(exchange: "logitrack.exchange", type: ExchangeType.Topic, durable: true);
            var body = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(@event));
            channel.BasicPublish(exchange: "logitrack.exchange", routingKey: topic, basicProperties: null, body: body);
        });
        
        return Task.CompletedTask;
    }
}

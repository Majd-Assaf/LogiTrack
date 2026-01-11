using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Polly;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RabbitMQ.Client.Exceptions;

namespace TrackingService.Worker;

public class RabbitMqSubscriber
{
    private readonly IConfiguration _config;
    private readonly ConnectionFactory _factory;
    private readonly ILogger<RabbitMqSubscriber> _logger;

    public RabbitMqSubscriber(IConfiguration config, ILogger<RabbitMqSubscriber> logger)
    {
        _config = config;
        _logger = logger;
        _factory = new ConnectionFactory
        {
            HostName = _config.GetValue<string>("RabbitMQ:Host") ?? "rabbitmq",
            UserName = _config.GetValue<string>("RabbitMQ:User") ?? "guest",
            Password = _config.GetValue<string>("RabbitMQ:Password") ?? "guest"
        };
    }

    public void Subscribe(string routingKey, Func<string, Task> onMessage)
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
            var conn = _factory.CreateConnection();
            var channel = conn.CreateModel();
            channel.ExchangeDeclare("logitrack.exchange", ExchangeType.Topic, durable: true);

            var queueName = channel.QueueDeclare().QueueName;
            channel.QueueBind(queueName, "logitrack.exchange", routingKey);

            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += async (sender, ea) =>
            {
                var body = Encoding.UTF8.GetString(ea.Body.ToArray());
                try
                {
                    await onMessage(body);
                    channel.BasicAck(ea.DeliveryTag, false);
                }
                catch
                {
                    channel.BasicNack(ea.DeliveryTag, false, true);
                }
            };
            channel.BasicConsume(queueName, false, consumer);
        });
    }
}

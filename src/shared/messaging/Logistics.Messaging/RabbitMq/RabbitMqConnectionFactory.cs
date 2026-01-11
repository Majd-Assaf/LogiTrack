using RabbitMQ.Client;
using System.Threading.Tasks;

namespace Logistics.Messaging.RabbitMq;

public sealed class RabbitMqConnectionFactory
{
    private readonly ConnectionFactory _factory;

    public RabbitMqConnectionFactory(Configuration.RabbitMqOptions options)
    {
        _factory = new ConnectionFactory
        {
            HostName = options.Host,
            Port = options.Port,
            VirtualHost = options.VirtualHost,
            UserName = options.UserName,
            Password = options.Password,
            //DispatchConsumersAsync = true
        };
    }

    public async Task<IConnection> CreateConnection() => await _factory.CreateConnectionAsync();
}

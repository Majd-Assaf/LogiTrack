using Logistics.Messaging.Abstractions;
using Logistics.Messaging.Configuration;
using Logistics.Messaging.RabbitMq;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Logistics.Messaging.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddLogisticsMessaging(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        var options = configuration
            .GetSection("RabbitMq")
            .Get<RabbitMqOptions>()!;

        services.AddSingleton(options);
        services.AddSingleton<RabbitMqConnectionFactory>();
        services.AddSingleton<IMessagePublisher, RabbitMqPublisher>();

        return services;
    }
}

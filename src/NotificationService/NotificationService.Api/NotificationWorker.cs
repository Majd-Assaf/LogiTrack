using System.Text.Json;
using Microsoft.Extensions.Hosting;

namespace NotificationService;

public record TransportCreatedEvent(Guid TransportId, string OrderNumber, DateTime CreatedAt);

public class NotificationWorker : BackgroundService
{
    private readonly RabbitMqSubscriber _sub;
    public NotificationWorker(RabbitMqSubscriber sub) => _sub = sub;

    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _sub.Subscribe("transport.created", async body =>
        {
            var evt = JsonSerializer.Deserialize<TransportCreatedEvent>(body);
            if (evt != null)
            {
                // Simulierter Versand: Console/Log - später Webhook oder E-Mail integrieren
                Console.WriteLine($"[Notification] Transport created: {evt.TransportId} / {evt.OrderNumber}");
            }
            await Task.CompletedTask;
        });

        return Task.CompletedTask;
    }
}

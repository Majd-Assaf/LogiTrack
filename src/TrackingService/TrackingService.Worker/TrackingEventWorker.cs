using System.Text.Json;
using Microsoft.Extensions.Hosting;

namespace TrackingService.Worker;

public record TransportCreatedEvent(Guid TransportId, string OrderNumber, DateTime CreatedAt);

public class TrackingEventWorker : BackgroundService
{
    private readonly RabbitMqSubscriber _subscriber;

    public TrackingEventWorker(RabbitMqSubscriber subscriber)
    {
        _subscriber = subscriber;
    }

    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _subscriber.Subscribe("transport.created", async body =>
        {
            var evt = JsonSerializer.Deserialize<TransportCreatedEvent>(body);
            if (evt != null)
            {
                Console.WriteLine($"[Tracking] Transport created: {evt.TransportId} / {evt.OrderNumber}");
                // Hier z.B. Indexieren in ES oder Speicherung in DB möglich
            }
            await Task.CompletedTask;
        });

        return Task.CompletedTask;
    }
}

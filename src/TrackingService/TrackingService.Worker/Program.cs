using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TrackingService.Worker;

var host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((ctx, services) =>
    {
        services.AddSingleton<RabbitMqSubscriber>();
        services.AddHostedService<TrackingEventWorker>();
    })
    .Build();

await host.RunAsync();

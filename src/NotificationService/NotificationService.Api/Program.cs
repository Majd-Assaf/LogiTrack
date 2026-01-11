using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NotificationService;

var host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((ctx, services) =>
    {
        services.AddSingleton<RabbitMqSubscriber>();
        services.AddHostedService<NotificationWorker>();
    })
    .Build();

await host.RunAsync();

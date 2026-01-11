using Microsoft.EntityFrameworkCore;
using Polly;
using System;
using TransportService.Infrastructure;
using TransportService.Infrastructure.Messaging;
using Microsoft.Extensions.Logging;

var builder = WebApplication.CreateBuilder(args);

// Konfiguration laden (appsettings.json)
builder.Configuration.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

// DB-Kontext konfigurieren
var conn = builder.Configuration.GetConnectionString("DefaultConnection")
           ?? "server=mysql-0;port=3306;database=logitrack;uid=app;pwd=app";
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySql(conn, ServerVersion.AutoDetect(conn)));

// DI
builder.Services.AddScoped<ITransportRepository, TransportRepository>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddSingleton<IEventPublisher, RabbitMqEventPublisher>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Ensure DB created in development
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var logger = services.GetRequiredService<ILogger<Program>>();
    var db = services.GetRequiredService<AppDbContext>();

    var retryPolicy = Policy
        .Handle<Exception>()
        .WaitAndRetry(new[]
        {
            TimeSpan.FromSeconds(5),
            TimeSpan.FromSeconds(10),
            TimeSpan.FromSeconds(15)
        }, (exception, timeSpan, retryCount, context) =>
        {
            logger.LogWarning(exception, "Error connecting to DB. Retrying in {timeSpan}. Attempt {retryCount}", timeSpan, retryCount);
        });

    retryPolicy.Execute(() =>
    {
        db.Database.EnsureCreated();
    });
}

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "TransportService.Api V1");
    c.RoutePrefix = string.Empty;
});

app.MapControllers();

app.Run();

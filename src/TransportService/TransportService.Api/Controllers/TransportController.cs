using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography.Xml;
using TransportService.Domain;
using TransportService.Infrastructure;
using TransportService.Infrastructure.Messaging;

namespace TransportService.Api.Controllers;

[ApiController]
[Route("api/transports")]
public class TransportController : ControllerBase
{
    private readonly ITransportRepository _repo;
    private readonly IUnitOfWork _uow;
    private readonly IEventPublisher _publisher;

    public TransportController(ITransportRepository repo, IUnitOfWork uow, IEventPublisher publisher)
    {
        _repo = repo;
        _uow = uow;
        _publisher = publisher;
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateTransportRequest request)
    {
        var t = new Transport(request.OrderNumber, request.ReceiverAddress);
        await _repo.AddAsync(t);
        await _uow.SaveChangesAsync();

        var evt = new TransportCreatedEvent { TransportId = t.Id, OrderNumber = t.OrderNumber, CreatedAt = t.CreatedAt };
        await _publisher.PublishAsync("transport.created", evt);

        return Ok(new { t.Id });
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> Get(Guid id)
    {
        var t = await _repo.GetAsync(id);
        if (t == null) return NotFound();
        return Ok(t);
    }
}

public record CreateTransportRequest(string OrderNumber, string ReceiverAddress);

public class TransportCreatedEvent
{
    public Guid TransportId { get; set; }
    public string OrderNumber { get; set; } = null!;
    public DateTime CreatedAt { get; set; }
}

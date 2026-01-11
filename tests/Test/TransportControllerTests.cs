using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Threading.Tasks;
using TransportService.Api.Controllers;
using TransportService.Domain;
using TransportService.Infrastructure;
using Xunit;

public class TransportControllerTests
{
    [Fact]
    public async Task Create_ReturnsOk_WithId()
    {
        var repoMock = new Mock<ITransportRepository>();
        var uowMock = new Mock<IUnitOfWork>();
        var pubMock = new Mock<TransportService.Infrastructure.Messaging.IEventPublisher>();

        repoMock.Setup(r => r.AddAsync(It.IsAny<Transport>())).Returns(Task.CompletedTask);
        uowMock.Setup(u => u.SaveChangesAsync()).Returns(Task.CompletedTask);
        pubMock.Setup(p => p.PublishAsync(It.IsAny<string>(), It.IsAny<object>())).Returns(Task.CompletedTask);

        var controller = new TransportController(repoMock.Object, uowMock.Object, pubMock.Object);
        var result = await controller.Create(new CreateTransportRequest("ORD-1", "Test Street 1")) as OkObjectResult;
        Assert.NotNull(result);
    }

    [Fact]
    public async Task Get_ReturnsNotFound_WhenNotExists()
    {
        var repoMock = new Mock<ITransportRepository>();
        var uowMock = new Mock<IUnitOfWork>();
        var pubMock = new Mock<TransportService.Infrastructure.Messaging.IEventPublisher>();

        repoMock.Setup(r => r.GetAsync(It.IsAny<Guid>())).ReturnsAsync((Transport)null);

        var controller = new TransportController(repoMock.Object, uowMock.Object, pubMock.Object);
        var result = await controller.Get(Guid.NewGuid());
        Assert.IsType<NotFoundResult>(result);
    }
}

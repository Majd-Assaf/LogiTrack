namespace TransportService.Domain;

public enum TransportStatus { Created, PickedUp, InTransit, Delivered }

public class Transport
{
    public Guid Id { get; private set; }
    public string OrderNumber { get; private set; } = null!;
    public string ReceiverAddress { get; private set; } = null!;
    public TransportStatus Status { get; private set; }
    public DateTime CreatedAt { get; private set; }

    public Transport(string orderNumber, string receiverAddress)
    {
        Id = Guid.NewGuid();
        OrderNumber = orderNumber;
        ReceiverAddress = receiverAddress;
        Status = TransportStatus.Created;
        CreatedAt = DateTime.UtcNow;
    }

    public void UpdateStatus(TransportStatus status) => Status = status;
}

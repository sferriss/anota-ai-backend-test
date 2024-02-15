namespace Catalog.Domain.Notifications;

public class SqsMessage
{
    public string? MessageId { get; set; }
    
    public string? Message { get; set; }
    
    public DateTime? Timestamp { get; set; }
}
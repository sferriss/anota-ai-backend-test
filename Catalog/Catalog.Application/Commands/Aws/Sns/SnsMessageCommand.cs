using Catalog.Domain.Notifications.Enums;
using MediatR;

namespace Catalog.Application.Commands.Aws.Sns;

public class SnsMessageCommand : IRequest
{
    public required string OwnerId { get; init; }
    
    public required string ItemId  { get; init; }
    
    public required OperationType Type  { get; init; }
    
    public required ItemType ItemType  { get; init; }
}
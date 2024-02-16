using MediatR;

namespace Catalog.Application.Commands.Aws.Sns;

public class SnsMessageCommand : IRequest
{
    public required string OwnerId { get; init; }
}
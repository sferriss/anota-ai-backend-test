using MediatR;

namespace Catalog.Application.Commands.Aws.Sns;

public class SnsMessageCommand : IRequest
{
    public SnsMessageCommand(string ownerId)
    {
        OwnerId = ownerId;
    }

    public string OwnerId { get; init; }
}
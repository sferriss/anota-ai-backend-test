using Amazon.SimpleNotificationService;
using Amazon.SimpleNotificationService.Model;
using MediatR;

namespace Catalog.Application.Commands.Aws.Sns;

public class SnsMessageCommandHandler(IAmazonSimpleNotificationService notificationService)
    : IRequestHandler<SnsMessageCommand>
{
    private readonly string? _topicArn = Environment.GetEnvironmentVariable("AWS__SNS__TOPIC_ARN");

    public async Task Handle(SnsMessageCommand request, CancellationToken cancellationToken)
    {
        var message = new PublishRequest
        {
            TopicArn = _topicArn,
            Message = request.OwnerId,
        };
        await notificationService.PublishAsync(message, cancellationToken).ConfigureAwait(false);
    }
}
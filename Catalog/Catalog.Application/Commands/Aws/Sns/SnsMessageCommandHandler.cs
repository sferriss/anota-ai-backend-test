using System.Text.Json;
using System.Text.Json.Serialization;
using Amazon.SimpleNotificationService;
using Amazon.SimpleNotificationService.Model;
using Catalog.Application.Mappers;
using MediatR;

namespace Catalog.Application.Commands.Aws.Sns;

public class SnsMessageCommandHandler(IAmazonSimpleNotificationService notificationService, NotificationMapper mapper)
    : IRequestHandler<SnsMessageCommand>
{
    private readonly string? _topicArn = Environment.GetEnvironmentVariable("AWS__SNS__TOPIC_ARN");

    public async Task Handle(SnsMessageCommand request, CancellationToken cancellationToken)
    {
        var jsonString = SerializeAsString(request);

        var message = new PublishRequest
        {
            TopicArn = _topicArn,
            Message = jsonString,
        };
        await notificationService.PublishAsync(message, cancellationToken).ConfigureAwait(false);
    }

    private string SerializeAsString(SnsMessageCommand request)
    {
        var options = new JsonSerializerOptions
        {
            Converters = { new JsonStringEnumConverter(JsonNamingPolicy.CamelCase) }
        };

        var jsonString = JsonSerializer.Serialize(mapper.ToNotification(request), options);
        return jsonString;
    }
}
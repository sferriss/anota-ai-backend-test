using System.Text.Json;
using System.Text.Json.Serialization;
using Amazon.SQS;
using Amazon.SQS.Model;
using Catalog.Application.Commands.Files.Update;
using Catalog.Domain.Notifications;

namespace Catalog.Emit.Consumer;

public class Worker(ILogger<Worker> logger, IAmazonSQS sqsClient, IUpdateJsonFileCommandHandler jsonFileCommandHandler) : BackgroundService
{
    private readonly string? _queueUrl = Environment.GetEnvironmentVariable("AWS__SQS__QUEUE_NAME");

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                var response = await ReceiveMessageAsync(stoppingToken).ConfigureAwait(false);

                foreach (var message in response.Messages)
                {
                    var sqsMessage = DeserializeMessage(message, out var messageContent);

                    logger.LogInformation("Mensagem recebida: {MessageId}", sqsMessage!.MessageId);
                    
                    await jsonFileCommandHandler.ExecuteAsync(messageContent!).ConfigureAwait(false);

                    await DeleteMessageAsync(message, stoppingToken).ConfigureAwait(false);
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Ocorreu um erro ao processar a mensagem.");
            }

            await Task.Delay(TimeSpan.FromSeconds(5), stoppingToken);
        }
    }

    private static SqsMessage? DeserializeMessage(Message message, out Notification? messageContent)
    {
        var options = new JsonSerializerOptions
        {
            Converters = { new JsonStringEnumConverter(JsonNamingPolicy.CamelCase) }
        };
                    
        var sqsMessage = JsonSerializer.Deserialize<SqsMessage>(message.Body);
        messageContent = JsonSerializer.Deserialize<Notification>(sqsMessage?.Message!, options);
        return sqsMessage;
    }

    private async Task<ReceiveMessageResponse> ReceiveMessageAsync(CancellationToken stoppingToken)
    {
        var receiveRequest = new ReceiveMessageRequest
        {
            QueueUrl = _queueUrl,
            MaxNumberOfMessages = 1,
            WaitTimeSeconds = 20
        };

        var response = await sqsClient.ReceiveMessageAsync(receiveRequest, stoppingToken).ConfigureAwait(false);
        return response;
    }

    private async Task DeleteMessageAsync(Message message, CancellationToken stoppingToken)
    {
        var deleteRequest = new DeleteMessageRequest
        {
            QueueUrl = _queueUrl,
            ReceiptHandle = message.ReceiptHandle
        };
        await sqsClient.DeleteMessageAsync(deleteRequest, stoppingToken);
    }
}
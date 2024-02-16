using System.Text.Json;
using System.Text.Json.Serialization;
using Amazon.SQS;
using Amazon.SQS.Model;
using Catalog.Domain.Files.Commands;
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
                    var sqsMessage = JsonSerializer.Deserialize<SqsMessage>(message.Body);

                    logger.LogInformation("Mensagem recebida: {MessageId}", sqsMessage!.MessageId);
                    
                    await jsonFileCommandHandler.ExecuteAsync(sqsMessage.Message!).ConfigureAwait(false);

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
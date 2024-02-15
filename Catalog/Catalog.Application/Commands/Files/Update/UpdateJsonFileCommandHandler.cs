using Amazon.S3;
using Catalog.Domain.Notifications;

namespace Catalog.Application.Commands.Files.Update;

public class UpdateJsonFileCommandHandler(IAmazonS3 amazonS3) : IUpdateJsonFileCommandHandler
{
    public Task ExecuteAsync(Notification notification)
    {
        return Task.CompletedTask;
    }
}
using Catalog.Domain.Notifications;

namespace Catalog.Application.Commands.Files.Update;

public interface IUpdateJsonFileCommandHandler
{
    Task ExecuteAsync(Notification notification);
}
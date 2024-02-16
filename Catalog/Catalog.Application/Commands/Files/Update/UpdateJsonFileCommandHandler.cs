using Catalog.Domain.Files.Commands;

namespace Catalog.Application.Commands.Files.Update;

public class UpdateJsonFileCommandHandler() : IUpdateJsonFileCommandHandler
{
    public Task ExecuteAsync(string ownerId)
    {
        return Task.CompletedTask;
    }
}
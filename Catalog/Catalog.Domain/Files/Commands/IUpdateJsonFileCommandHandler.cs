namespace Catalog.Domain.Files.Commands;

public interface IUpdateJsonFileCommandHandler
{
    Task ExecuteAsync(string ownerId);
}
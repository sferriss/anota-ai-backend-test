using System.Text.Json;
using Catalog.Application.Commands.Aws.S3;
using Catalog.Application.Mappers;
using Catalog.Domain.Categories.Repositories;
using Catalog.Domain.Files;
using Catalog.Domain.Files.Commands;

namespace Catalog.Application.Commands.Files.Update;

public class UpdateJsonFileCommandHandler(
    ICategoryRepository repository,
    IS3Services services,
    FileMapper fileMapper) : IUpdateJsonFileCommandHandler
{
    public async Task ExecuteAsync(string ownerId)
    {
        var categoriesAggregations = await repository.GetWithProductsByOwnerAsync(ownerId)
            .ConfigureAwait(false);

        var catalog = categoriesAggregations.Select(fileMapper.ToCatalogDto).ToArray();
        var file = new FileDto(ownerId, catalog);

        var fileName = $"{ownerId}.json";
        var hasFile = await services.ExistsFileAsync(fileName).ConfigureAwait(false);

        if (hasFile)
        {
            await services.DeleteFileAsync(fileName).ConfigureAwait(false);
        }
        
        var json = JsonSerializer.Serialize(file);
        await services.UploadFileAsync(fileName, json);
    }
}
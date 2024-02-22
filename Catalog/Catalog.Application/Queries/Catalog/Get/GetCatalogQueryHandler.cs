using System.Text.Json;
using Amazon.SimpleNotificationService.Model;
using Catalog.Application.Commands.Aws.S3;
using Catalog.Domain.Files;
using MediatR;

namespace Catalog.Application.Queries.Catalog.Get;

public class GetCatalogQueryHandler (IS3Services s3Services) : IRequestHandler<GetCatalogQuery, GetCatalogQueryResult>
{
    public async Task<GetCatalogQueryResult> Handle(GetCatalogQuery request, CancellationToken cancellationToken)
    {
        var fileName = $"{request.OwnerId}.json";
        var hasFile = await s3Services.ExistsFileAsync(fileName).ConfigureAwait(false);

        if (!hasFile) throw new NotFoundException("Catalog not found");
        
        var file = await s3Services.GetFileAsync(fileName).ConfigureAwait(false);

        var result = JsonSerializer.Deserialize<FileDto>(file);
        
        return new(result!);
    }
}
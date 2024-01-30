using Catalog.Domain.Options;

namespace Catalog.Api.Options;

public class MongoDbOptions : IMongoDbOptions
{
    public string ConnectionString { get; set; } = null!;
    
    public string DataBaseName { get; set; } = null!;
}
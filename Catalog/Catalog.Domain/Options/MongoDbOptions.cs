namespace Catalog.Domain.Options;

public class MongoDbOptions : IMongoDbOptions
{
    public string ConnectionString { get; set; } = null!;
    
    public string DataBaseName { get; set; } = null!;
}
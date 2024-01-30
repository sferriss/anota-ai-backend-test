namespace Catalog.Domain.Options;

public interface IMongoDbOptions
{
    public string ConnectionString { get; set; }
    
    public string DataBaseName { get; set; }
}
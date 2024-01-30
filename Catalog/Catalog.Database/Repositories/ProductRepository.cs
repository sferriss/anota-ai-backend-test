using Catalog.Database.Abstractions;
using Catalog.Database.Configurations;
using Catalog.Domain.Options;
using Catalog.Domain.Products;
using Catalog.Domain.Products.Repositories;
using MongoDB.Driver;

namespace Catalog.Database.Repositories;

public class ProductRepository : RepositoryBase<Product>, IProductRepository
{
    public ProductRepository(IMongoDbOptions options)
         {
             _ = options ?? throw new ArgumentException();
     
             var client = new MongoClient(options.ConnectionString);
             var database = client.GetDatabase(options.DataBaseName);
     
             Collection = database.GetCollection<Product>(CollectionNames.Product);
         }
}
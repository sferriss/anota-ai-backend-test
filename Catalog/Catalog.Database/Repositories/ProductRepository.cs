using Catalog.Database.Abstractions;
using Catalog.Database.Configurations;
using Catalog.Domain.Aggregations;
using Catalog.Domain.Options;
using Catalog.Domain.Products;
using Catalog.Domain.Products.Repositories;
using MongoDB.Bson;
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

    public Task<List<ProductWithCategoryResult>> GetWithCategoriesAsync(string owner)
    {
        var pipeline = new BsonDocument[]
        {
            new("$match", new BsonDocument
            {
                { "owner", $"{owner}" }
            }),
            new("$lookup", new BsonDocument
            {
                { "from", "categories" },
                { "localField", "categoryId" },
                { "foreignField", "_id" },
                { "as", "category" }
            }),
            new("$unwind", "$category"),
            new("$project", new BsonDocument
            {
                { "_id", new BsonDocument("$toString", "$_id") },
                { "Title", "$title" },
                { "Owner", "$owner" },
                { "Description", "$description" },
                { "Price", "$price" },
                {
                    "Category", new BsonDocument
                    {
                        { "_id", new BsonDocument("$toString", "$categoryId") },
                        { "Title", "$category.title" },
                        { "Owner", "$category.owner" },
                        { "Description", "$category.description" }
                    }
                }
            })
        };

        var aggregateResult = Collection.Aggregate<ProductWithCategoryResult>(pipeline).ToList();


        return Task.FromResult(aggregateResult);
    }
}
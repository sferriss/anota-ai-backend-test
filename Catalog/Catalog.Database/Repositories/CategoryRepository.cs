using Catalog.Database.Abstractions;
using Catalog.Database.Configurations;
using Catalog.Domain.Categories;
using Catalog.Domain.Categories.Repositories;
using Catalog.Domain.Aggregations;
using Catalog.Domain.Options;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Catalog.Database.Repositories;

public class CategoryRepository : RepositoryBase<Category>, ICategoryRepository
{
    public CategoryRepository(IMongoDbOptions options)
    {
        _ = options ?? throw new ArgumentException();

        var client = new MongoClient(options.ConnectionString);
        var database = client.GetDatabase(options.DataBaseName);

        Collection = database.GetCollection<Category>(CollectionNames.Category);
    }

    public async Task<bool> HasWithTitleAsync(string title)
    {
        return await Collection
            .Find(Builders<Category>.Filter.Eq(x => x.Title, title))
            .AnyAsync();
    }

    public Task<List<CategoryWithProducts>> GetWithProductsByOwnerAsync(string owner)
    {
        var pipeline = new[]
        {
            new BsonDocument("$match", new BsonDocument
            {
                { "owner", $"{owner}" }
            }),
            new BsonDocument("$lookup", new BsonDocument
            {
                { "from", "products" },
                { "localField", "_id" },
                { "foreignField", "categoryId" },
                { "as", "products" }
            }),
            new BsonDocument("$project", new BsonDocument
            {
                { "_id", new BsonDocument("$toString", "$_id") },
                { "Title", "$title" },
                { "Owner", "$owner" },
                { "Description", "$description" },
                {
                    "Products", new BsonDocument
                    {
                        {
                            "$map", new BsonDocument
                            {
                                { "input", "$products" },
                                { "as", "product" },
                                {
                                    "in", new BsonDocument
                                    {
                                        { "_id", new BsonDocument("$toString", "$$product._id") },
                                        { "Title", "$$product.title" },
                                        { "Owner", "$$product.owner" },
                                        { "Description", "$$product.description" },
                                        { "Price", "$$product.price" },
                                        { "CategoryId", new BsonDocument("$toString", "$$product.categoryId") }
                                    }
                                }
                            }
                        }
                    }
                }
            })
        };

        var aggregateResult = Collection.Aggregate<CategoryWithProducts>(pipeline).ToList();

        return Task.FromResult(aggregateResult);
    }
}
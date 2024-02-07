using Catalog.Database.Configurations;
using Catalog.Domain.Aggregations;
using Catalog.Domain.Aggregations.Repositories;
using Catalog.Domain.Categories;
using Catalog.Domain.Options;
using Catalog.Domain.Products;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Catalog.Database.Repositories;

public class AggregationsRepository : IAggregationsRepository
{
    private readonly IMongoCollection<Product> _productCollection;
    private readonly IMongoCollection<Category> _categoryCollection;
    
    public AggregationsRepository(IMongoDbOptions options)
    {
        _ = options ?? throw new ArgumentException();
     
        var client = new MongoClient(options.ConnectionString);
        var database = client.GetDatabase(options.DataBaseName);
     
        _productCollection = database.GetCollection<Product>(CollectionNames.Product);
        _categoryCollection = database.GetCollection<Category>(CollectionNames.Category);
    }
    
    public async Task<List<ProductWithCategoryResult>> GetProductsWithCategoriesAsync()
    {
        var pipeline = new[]
        {
            new BsonDocument("$lookup",
                new BsonDocument
                {
                    { "from", "category" },
                    { "localField", "categoryId" },
                    { "foreignField", "_id" },
                    { "as", "category" }
                }),
            
            new BsonDocument("$project",
                new BsonDocument
                {
                    { "_id", new BsonDocument("$toString", "$_id") },
                    { "Title", "$title" },
                    { "Price", "$price" },
                    { "Description", "$description" },
                    { "Owner", "$owner" },
                    { "Category._id", new BsonDocument("$toString", "$categoryId") },
                    { "Category.Title", "$title" },
                    { "Category.Owner", "$owner" },
                    { "Category.Description", "$description" }
                })
        };

        var productsWithCategories = await _productCollection
            .Aggregate<ProductWithCategoryResult>(pipeline)
            .ToListAsync();

        return productsWithCategories;
    }
}
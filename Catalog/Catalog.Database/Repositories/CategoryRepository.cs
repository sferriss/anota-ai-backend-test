using Catalog.Database.Abstractions;
using Catalog.Database.Configurations;
using Catalog.Domain.Categories;
using Catalog.Domain.Categories.Repositories;
using Catalog.Domain.Options;
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

    public async Task<bool> HasCategoryWithTitleAsync(string title)
    {
        return await Collection
            .Find(Builders<Category>.Filter.Eq(x => x.Title, title))
            .AnyAsync();
    }
}
using Catalog.Domain.Abstractions;
using MongoDB.Driver;

namespace Catalog.Database.Abstractions;

public class RepositoryBase<T> : IRepositoryBase<T> where T : class, IEntity<string>
{
    protected IMongoCollection<T> Collection = null!;
        
    public async Task<T> GetAsync(string id)
    {
        return await Collection
            .Find(Builders<T>.Filter.Eq(x => x.Id, id))
            .FirstOrDefaultAsync();
    }

    public T Update(T entity)
    {
        return Collection.FindOneAndReplace(Builders<T>.Filter.Eq(x => x.Id, entity.Id), entity);
    }

    public T Add(T entity)
    {
        Collection.InsertOne(entity);
        
        return entity;
    }

    public void Remove(T entity)
    {
        Collection.FindOneAndDelete(Builders<T>.Filter.Eq(x => x.Id, entity.Id));
    }

    public async Task<IList<T>> GetAllAsync()
    {
        return await Collection.Find(Builders<T>.Filter.Empty).ToListAsync();
    }
}
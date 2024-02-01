namespace Catalog.Domain.Abstractions;

public interface IRepositoryBase<T> where T : class, IEntity<string>
{
    Task<T> GetAsync(string id);
    
    T Update(T entity);
    
    T Add(T entity);
    
    void Remove(T entity);
    
    Task<IList<T>> GetAllAsync();
}
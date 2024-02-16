using Catalog.Domain.Abstractions;
using Catalog.Domain.Aggregations;

namespace Catalog.Domain.Products.Repositories;

public interface IProductRepository : IRepositoryBase<Product>
{
    Task<List<ProductWithCategoryResult>> GetWithCategoriesAsync(string owner);
}
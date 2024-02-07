namespace Catalog.Domain.Aggregations.Repositories;

public interface IAggregationsRepository
{
    Task<List<ProductWithCategoryResult>> GetProductsWithCategoriesAsync();
}
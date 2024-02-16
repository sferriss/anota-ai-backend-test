using Catalog.Domain.Abstractions;
using Catalog.Domain.Aggregations;

namespace Catalog.Domain.Categories.Repositories;

public interface ICategoryRepository : IRepositoryBase<Category>
{
    Task<bool> HasWithTitleAsync(string title);

    Task<List<CategoryWithProducts>> GetWithProductsByOwnerAsync(string owner);
}
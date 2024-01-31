using Catalog.Domain.Abstractions;

namespace Catalog.Domain.Categories.Repositories;

public interface ICategoryRepository : IRepositoryBase<Category>
{
    Task<bool> HasCategoryWithTitleAsync(string title);
}
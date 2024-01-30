using Catalog.Database.Configurations;
using Catalog.Database.Repositories;
using Catalog.Domain.Categories.Repositories;
using Catalog.Domain.Products.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Catalog.Application.Extensions;

public static class DependencyInjectionExtensions
{
    public static void ConfigurationMongoDb(this IServiceCollection _)
    {
        MongoDbMapping.ConfigureMappings();
    }
    
    public static void AddRepositories(this IServiceCollection services)
    {
        services.AddTransient<IProductRepository, IProductRepository>();
        services.AddTransient<ICategoryRepository, CategoryRepository>();
    }
}
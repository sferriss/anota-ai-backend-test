using System.Reflection;
using Catalog.Application.Commands.Categories.Create;
using Catalog.Application.Mappers;
using Catalog.Database.Configurations;
using Catalog.Database.Repositories;
using Catalog.Domain.Categories.Repositories;
using Catalog.Domain.Products.Repositories;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;

namespace Catalog.Application.Extensions;

public static class DependencyInjectionExtensions
{
    public static void AddApplicationDependencies(this IServiceCollection services)
    {
        services.AddMediatR(cfg =>
            cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));
        
        services.AddFluentValidationAutoValidation().AddFluentValidationClientsideAdapters();
        services.AddValidatorsFromAssemblyContaining<CreateCategoryCommandValidator>();
        
        MongoDb.Configure();
        services.AddRepositories();
        services.AddMappers();
    }
    
    private static void AddRepositories(this IServiceCollection services)
    {
        services.AddTransient<IProductRepository, ProductRepository>();
        services.AddTransient<ICategoryRepository, CategoryRepository>();
    }
    
    private static void AddMappers(this IServiceCollection services)
    {
        services.AddTransient<CategoryMapper>();
        services.AddTransient<ProductMapper>();
        services.AddTransient<NotificationMapper>();
    }
}
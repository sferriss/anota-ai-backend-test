﻿using System.Reflection;
using Catalog.Application.Commands.Aws.S3;
using Catalog.Application.Commands.Categories.Create;
using Catalog.Application.Commands.Files.Update;
using Catalog.Application.Mappers;
using Catalog.Database.Configurations;
using Catalog.Database.Repositories;
using Catalog.Domain.Categories.Repositories;
using Catalog.Domain.Files.Commands;
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
        services.AddTransient<IS3Services, S3Services>();
        
        MongoDb.Configure();
        services.AddRepositories();
        services.AddMappers();
    }
    
    public static void AddWorkerDependencies(this IServiceCollection services)
    {
        services.AddTransient<IUpdateJsonFileCommandHandler, UpdateJsonFileCommandHandler>();
        services.AddTransient<IS3Services, S3Services>();
        
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
        services.AddTransient<FileMapper>();
    }
}
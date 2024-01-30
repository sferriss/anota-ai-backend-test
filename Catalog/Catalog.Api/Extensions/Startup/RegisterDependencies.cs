using Catalog.Api.Options;
using Catalog.Application.Extensions;
using Catalog.Domain.Options;
using Microsoft.Extensions.Options;

namespace Catalog.Api.Extensions.Startup;

public static class RegisterDependencies
{
    public static void RegisterOptions(this WebApplicationBuilder builder)
    {
        builder.Services.Configure<MongoDbOptions>(builder.Configuration.GetSection("MongoDB"));

        builder.Services.AddSingleton<IMongoDbOptions>(x => x.GetRequiredService<IOptions<MongoDbOptions>>().Value);
    }
    
    public static void RegisterApplicationDependencies(this WebApplicationBuilder builder)
    {
        builder.Services.ConfigurationMongoDb();
        builder.Services.AddRepositories();
    }
}
using Amazon.S3;
using Amazon.SimpleNotificationService;
using Amazon.SQS;
using Catalog.Api.Options;
using Catalog.Application.Extensions;
using Catalog.Domain.Options;
using Microsoft.Extensions.Options;

namespace Catalog.Api.Extensions.Startup;

public static class RegisterDependencies
{
    public static void RegisterOptions(this WebApplicationBuilder builder)
    {
        DotNetEnv.Env.Load();
        builder.Services.Configure<MongoDbOptions>(builder.Configuration.GetSection("MongoDB"));

        builder.Services.AddSingleton<IMongoDbOptions>(x => x.GetRequiredService<IOptions<MongoDbOptions>>().Value);
        
        builder.Services.AddDefaultAWSOptions(builder.Configuration.GetAWSOptions());
        builder.Services.AddAWSService<IAmazonS3>();
        builder.Services.AddAWSService<IAmazonSimpleNotificationService>(); 
        builder.Services.AddAWSService<IAmazonSQS>(); 
    }
    
    public static void RegisterApplicationDependencies(this WebApplicationBuilder builder)
    {
        builder.Services.AddApplicationDependencies();
    }
}
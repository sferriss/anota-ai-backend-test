using Amazon.S3;
using Amazon.SQS;
using Catalog.Application.Extensions;
using Catalog.Domain.Options;
using Catalog.Emit.Consumer;
using Microsoft.Extensions.Options;

var builder = Host.CreateApplicationBuilder(args);
DotNetEnv.Env.Load();
builder.Services.Configure<MongoDbOptions>(builder.Configuration.GetSection("MongoDB"));
builder.Services.AddSingleton<IMongoDbOptions>(x => x.GetRequiredService<IOptions<MongoDbOptions>>().Value);
builder.Services.AddDefaultAWSOptions(builder.Configuration.GetAWSOptions());
builder.Services.AddAWSService<IAmazonSQS>();
builder.Services.AddAWSService<IAmazonS3>();
builder.Services.AddHostedService<Worker>();
builder.Services.AddWorkerDependencies();

var host = builder.Build();
host.Run();
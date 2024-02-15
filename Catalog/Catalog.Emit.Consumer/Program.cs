using Amazon.S3;
using Amazon.SQS;
using Catalog.Application.Extensions;
using Catalog.Emit.Consumer;

var builder = Host.CreateApplicationBuilder(args);
DotNetEnv.Env.Load();
builder.Services.AddDefaultAWSOptions(builder.Configuration.GetAWSOptions());
builder.Services.AddAWSService<IAmazonSQS>();
builder.Services.AddAWSService<IAmazonS3>();
builder.Services.AddHostedService<Worker>();
builder.Services.AddWorkerDependencies();

var host = builder.Build();
host.Run();
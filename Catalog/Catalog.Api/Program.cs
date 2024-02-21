using Catalog.Api.ExceptionHandlers;
using Catalog.Api.ExceptionHandlers.Middlewares;
using Catalog.Api.Extensions.Startup;
using Catalog.Application.Exceptions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.RegisterOptions();
builder.RegisterApplicationDependencies();
builder.RegisterExceptionHandlers();
builder.AddExceptionHandlers()
    .AddHandler<BusinessValidationException, BusinessValidationExceptionHandler>()
    .AddHandler<NotFoundException, NotFoundExceptionHandler>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ExceptionMiddleware>();

app.UseHttpsRedirection();

app.MapControllers();

app.Run();
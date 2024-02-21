using System.Net.Mime;
using System.Text.Json;
using Catalog.Api.ExceptionHandlers.Factories;
using Catalog.Api.ExceptionHandlers.Responses;

namespace Catalog.Api.ExceptionHandlers.Middlewares;

public class ExceptionMiddleware(RequestDelegate next)
{
    private static readonly JsonSerializerOptions SerializerOptions = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
    };

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex);
        }
    }

    private static Task HandleExceptionAsync(HttpContext context, Exception ex)
    {
        var responseExceptionResult = GetResponseExceptionOrDefault(context, ex);

        var responseJson = JsonSerializer.Serialize(responseExceptionResult, SerializerOptions);

        context.Response.StatusCode = responseExceptionResult.Status;
        context.Response.ContentType = MediaTypeNames.Application.Json;
        return context.Response.WriteAsync(responseJson);
    }

    private static ExceptionResponse GetResponseExceptionOrDefault(HttpContext context, Exception ex)
    {
        ExceptionResponse? responseExceptionResult = null;
        var exceptionHandler = context.RequestServices.GetService<ExceptionHandlerFactory>();

        if (exceptionHandler is null)
        {
            return responseExceptionResult ?? new ExceptionResponse()
            {
                Type = "InternalError",
                Title = "Something's not right",
                Status = StatusCodes.Status500InternalServerError,
                TraceId = context.TraceIdentifier
            };
        }

        var handler = exceptionHandler.GetExceptionHandlerBy(context.RequestServices, ex.GetType()) ??
                      exceptionHandler.GetExceptionHandlerBy(context.RequestServices, typeof(Exception));

        if (handler is not null)
        {
            responseExceptionResult = handler.HandleException(ex, context.TraceIdentifier);
        }

        return responseExceptionResult ?? new ExceptionResponse
        {
            Type = "InternalError",
            Title = "Something's not right",
            Status = StatusCodes.Status500InternalServerError,
            TraceId = context.TraceIdentifier
        };
    }
}
using Catalog.Api.ExceptionHandlers.Interfaces;
using Catalog.Api.ExceptionHandlers.Responses;
using Catalog.Application.Exceptions;

namespace Catalog.Api.ExceptionHandlers;

public class NotFoundExceptionHandler : IExceptionHandler
{
    public ExceptionResponse HandleException(Exception ex, string traceId)
    {
        var exception = ex as NotFoundException;
            
        return new ExceptionResponse
        {
            Type = "NotFound",
            Title = exception?.Message,
            Status = StatusCodes.Status404NotFound,
            TraceId = traceId
        };
    }
}
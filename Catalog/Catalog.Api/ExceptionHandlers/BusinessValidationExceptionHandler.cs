using Catalog.Api.ExceptionHandlers.Interfaces;
using Catalog.Api.ExceptionHandlers.Responses;
using Catalog.Application.Exceptions;

namespace Catalog.Api.ExceptionHandlers;

public class BusinessValidationExceptionHandler : IExceptionHandler
{
    public ExceptionResponse HandleException(Exception ex, string traceId)
    {
        var exception = ex as BusinessValidationException;
            
        return new ExceptionResponse
        {
            Type = "BusinessValidation",
            Title = exception?.Message,
            Status = StatusCodes.Status400BadRequest,
            TraceId = traceId
        };
    }
}
using Catalog.Api.ExceptionHandlers.Responses;

namespace Catalog.Api.ExceptionHandlers.Interfaces;

public interface IExceptionHandler
{
    ExceptionResponse HandleException(Exception ex, string traceId);
}
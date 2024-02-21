namespace Catalog.Api.ExceptionHandlers.Responses;

public class ExceptionResponse
{
    public string? Type { get; set; }

    public string? Title { get; set; }

    public int Status { get; set; }

    public string? TraceId { get; set; }
}
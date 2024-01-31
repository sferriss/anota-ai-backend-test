namespace Catalog.Application.Exceptions;

public class BusinessValidationException(string message) : Exception(message);
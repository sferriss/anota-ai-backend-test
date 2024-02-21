using Catalog.Api.ExceptionHandlers.Interfaces;

namespace Catalog.Api.ExceptionHandlers.Factories;

public class ExceptionHandlerFactory
{
    private readonly Dictionary<Type, Type> _handlers = new();
    
    public ExceptionHandlerFactory AddHandler<TException, THandler>()
        where TException : Exception
        where THandler : IExceptionHandler
    {
        _handlers.Add(typeof(TException), typeof(THandler));
        return this;
    }
    
    public IExceptionHandler? GetExceptionHandlerBy(IServiceProvider serviceProvider, Type exceptionType)
    {
        ArgumentNullException.ThrowIfNull(serviceProvider);
        ArgumentNullException.ThrowIfNull(exceptionType);

        return _handlers.TryGetValue(exceptionType, out var instanceType)
            ? serviceProvider.GetRequiredService(instanceType) as IExceptionHandler
            : null;
    }
}